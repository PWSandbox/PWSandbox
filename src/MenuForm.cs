// This file is a part of PWSandbox ( https://github.com/yarb00/PWSandbox )
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PWSandbox;

public enum Theme
{
	Light,
	Dark
}

public partial class MenuForm : Form
{
	private Theme currentColorTheme = Theme.Dark;

	public MenuForm()
	{
		InitializeComponent();

		appVersionLabel.Text = $"v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
	}

	private void LoadMapFile(object sender, EventArgs e)
	{
		string[] mapLines;

		try
		{
			mapLines = File.ReadAllLines(mapFileLocationTextBox.Text);
		}
		catch (ArgumentException ex)
		{
			if (ex.ParamName != "path") throw;

			MessageBox.Show(
				"Please enter a valid path of the map file.",
				"PWSandbox",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);

			return;
		}
		catch (FileNotFoundException)
		{
			MessageBox.Show(
				$"File \"{mapFileLocationTextBox.Text}\" does not exist.",
				"PWSandbox",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);

			return;
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
						MessageBox.Show(
							$"File \"{mapFileLocationTextBox.Text}\" is not a valid PWSandbox map or it is a map designed for a newer/older version of PWSandbox.",
							"PWSandbox",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error,
							MessageBoxDefaultButton.Button1
						);

						return;
					}

				case 1:
					if (mapLines[0].TrimStart().StartsWith("(map: begin)", true, null))
					{
						mapLines[0] = mapLines[0].TrimStart().Remove(0, "(map: begin)".Length);
						continue;
					}
					else
					{
						MessageBox.Show(
							$"An error occured while parsing map \"{mapFileLocationTextBox.Text}\": expected \"(map: begin)\" after map header (\"?PWSandbox-Map 1.0;\"), but it was not found.",
							"PWSandbox",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error,
							MessageBoxDefaultButton.Button1
						);

						return;
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
						MessageBox.Show(
							$"An error occured while parsing map \"{mapFileLocationTextBox.Text}\": expected \"(map: end)\" in the end of file, but it was not found.",
							"PWSandbox",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error,
							MessageBoxDefaultButton.Button1
						);

						return;
					}
			}
		}

		int maxX = 0;
		for (int y = 0; y < mapLines.Length; y++)
			if (maxX < mapLines[y].Length)
				maxX = mapLines[y].Length;

		MapObject[,] mapObjects = new MapObject[mapLines.Length, maxX];

		for (int y = 0; y < mapLines.Length; y++)
		{
			for (int x = 0; x < mapLines[y].Length; x++)
			{
				mapObjects[y, x] = mapLines[y][x] switch
				{
					' ' => MapObject.Void,
					'!' => MapObject.Player,
					'=' => MapObject.Finish,
					'@' => MapObject.Wall,
					'#' => MapObject.FakeWall,
					'*' => MapObject.Barrier,
					_ => MapObject.Unknown,
				};
			}
		}

		PlayForm playForm = new(mapObjects);
		playForm.Show();
	}

	private void SwitchTheme(object sender, EventArgs e)
	{
		Theme newTheme = currentColorTheme == Theme.Light ? Theme.Dark : Theme.Light;

		Color backColor = Color.Transparent, foreColor = Color.Transparent;

		if (newTheme == Theme.Light)
		{
			backColor = Color.White;
			foreColor = Color.Black;
		}
		else if (newTheme == Theme.Dark)
		{
			backColor = Color.Black;
			foreColor = Color.White;
		}

		currentColorTheme = newTheme;

		BackColor = backColor;
		ForeColor = foreColor;
	}

	private void OpenAboutAppDialog(object sender, EventArgs e)
	{
		AboutForm aboutForm = new();
		aboutForm.ShowDialog();
	}
}
