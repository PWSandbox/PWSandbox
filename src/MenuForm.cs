// This file is a part of PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// PWSandbox is licensed under the MIT (Expat) License:

/* MIT License
 *
 * Copyright (c) 2024 - 2025 yarb00
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PWSandbox;

public enum Theme
{
	SimpleDark,
	SimpleLight
}

public partial class MenuForm : Form
{
	public Theme CurrentColorTheme { get; private set; } = Theme.SimpleDark;
	public readonly Version? appVersion = null;

	public MenuForm()
	{
		InitializeComponent();

		appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

		if (appVersion is null) appVersionLabel.Text = "Missing version!";
		else appVersionLabel.Text = $"v{appVersion.ToString(3)}";
	}

	#region Map loading

	public static MapObject[,] ParseMapFromFile(string filePath)
	{
		string[] mapLines;

		try
		{
			mapLines = File.ReadAllLines(filePath);
		}
		catch (Exception)
		{
			throw;
		}

		for (int y = 0; y < 3; y++)
		{
			mapLines = mapLines.Where(str => !string.IsNullOrWhiteSpace(str)).ToArray();

			switch (y)
			{
				case 0:
					if (mapLines[0].TrimStart().StartsWith("?PWSandbox-Map 1.0;", true, null))
					{
						mapLines[0] = mapLines[0].TrimStart().Remove(0, "?PWSandbox-Map 1.0;".Length);
						continue;
					}
					else
					{
						throw new FormatException($"File \"{filePath}\" doesn't contain map header or version of standard in map header is unsupported");
					}

				case 1:
					if (mapLines[0].TrimStart().StartsWith("(map: begin)", true, null))
					{
						mapLines[0] = mapLines[0].TrimStart().Remove(0, "(map: begin)".Length);
						continue;
					}
					else
					{
						throw new FormatException($"Expected \"(map: begin)\" block after map header (\"?PWSandbox-Map 1.0;\") in file \"{filePath}\", but it was not found");
					}

				case 2:
					if (mapLines[^1].TrimEnd().EndsWith("(map: end)", true, null))
					{
						mapLines[^1] = mapLines[^1].TrimEnd().Remove(mapLines[^1].Length - "(map: end)".Length, "(map: end)".Length);
						mapLines = mapLines.Where(str => !string.IsNullOrWhiteSpace(str)).ToArray();

						break;
					}
					else
					{
						throw new FormatException($"Expected \"(map: end)\" block in the end of file \"{filePath}\", but it was not found");
					}
			}
		}

		int maxX = 0;
		for (int y = 0; y < mapLines.Length; y++)
			if (maxX < mapLines[y].Length)
				maxX = mapLines[y].Length;

		MapObject[,] mapObjects = new MapObject[mapLines.Length, maxX];

		for (int y = 0; y < mapLines.Length; y++)
			for (int x = 0; x < mapLines[y].Length; x++)
				mapObjects[y, x] = mapLines[y][x] switch
				{
					' ' => MapObject.Void,
					'!' => MapObject.Player,
					'=' => MapObject.Finish,
					'@' => MapObject.Wall,
					'#' => MapObject.FakeWall,
					'*' => MapObject.Barrier,
					_ => MapObject.Unknown
				};

		return mapObjects;
	}

	private void LoadMapFile(object sender, EventArgs e)
	{
		MapObject[,] mapObjects;

		if (mapOpenFileDialog.ShowDialog() == DialogResult.Cancel) return;

		string mapFileLocation = mapOpenFileDialog.FileName;

		try
		{
			mapObjects = ParseMapFromFile(mapFileLocation);
		}
		catch (FileNotFoundException)
		{
			MessageBox.Show(
				$"File \"{mapFileLocation}\" does not exist.",
				"PWSandbox",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);

			return;
		}
		catch (ArgumentException ex) when (ex.ParamName == "path")
		{
			MessageBox.Show(
				"Please enter a valid path of the map file.",
				"PWSandbox",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);

			return;
		}
		catch (FormatException ex) when (ex.Message.Contains("(map: end)", StringComparison.OrdinalIgnoreCase))
		{
			MessageBox.Show(
				$"An error occured while parsing map \"{mapFileLocation}\": expected \"(map: end)\" in the end of file, but it was not found.",
				"PWSandbox",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);

			return;
		}
		catch (FormatException ex) when (ex.Message.Contains("(map: begin)", StringComparison.OrdinalIgnoreCase))
		{
			MessageBox.Show(
				$"An error occured while parsing map \"{mapFileLocation}\": expected \"(map: begin)\" after map header (\"?PWSandbox-Map 1.0;\"), but it was not found.",
				"PWSandbox",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);

			return;
		}
		catch (FormatException ex) when (ex.Message.Contains("map header", StringComparison.OrdinalIgnoreCase))
		{
			MessageBox.Show(
				$"File \"{mapFileLocation}\" is not a valid PWSandbox map or it is a map designed for a newer/older version of PWSandbox.",
				"PWSandbox",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);

			return;
		}

		Dictionary<MapObject, Color>? colors =
			CurrentColorTheme == Theme.SimpleDark
				? new()
				{
					{ MapObject.Void, Color.Black },
					{ MapObject.Wall, Color.White },
					{ MapObject.FakeWall, Color.White },
					{ MapObject.Barrier, Color.Black }
				}
				: null;

		new PlayForm(mapObjects, colors).Show();
	}

	#endregion

	private void SwitchTheme(object sender, EventArgs e)
	{
		CurrentColorTheme =
			CurrentColorTheme == Theme.SimpleLight
				? Theme.SimpleDark
				: Theme.SimpleLight;

		Color
			backgroundColor = CurrentColorTheme switch
			{
				Theme.SimpleLight => Color.White,
				Theme.SimpleDark => Color.Black,
				_ => throw new NotImplementedException()
			},
			foregroundColor = CurrentColorTheme switch
			{
				Theme.SimpleLight => Color.Black,
				Theme.SimpleDark => Color.White,
				_ => throw new NotImplementedException()
			};

		BackColor =
		backgroundColor;

		ForeColor =
		foregroundColor;
	}

	private void OpenAboutAppDialog(object sender, EventArgs e)
	{
		new AboutForm(this).ShowDialog();
	}

	private void CheckForUpdates(object sender, EventArgs e)
	{
		new UpdateCheckForm(this).ShowDialog();
	}
}
