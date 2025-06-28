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

	private void LoadMapFile(object sender, EventArgs e)
	{
		if (mapOpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
		(PlayForm? playForm, string? errorText) = GetLoadedPlayForm(mapOpenFileDialog.FileName, CurrentColorTheme);

		if (errorText is not null) MessageBox.Show(
			errorText,
			"PWSandbox",
			MessageBoxButtons.OK,
			MessageBoxIcon.Error,
			MessageBoxDefaultButton.Button1
		);
		playForm?.Show();
	}

	public static (PlayForm? playForm, string? errorText) GetLoadedPlayForm(string mapFileLocation, Theme? colorTheme = null)
	{
		MapObject[,] mapObjects;
		try
		{
			mapObjects = MapParser.ParseMapFromFile(mapFileLocation);
		}
		catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
		{
			return (null, $"File \"{mapFileLocation}\" does not exist.");
		}
		catch (ArgumentException ex) when (ex.ParamName == "path")
		{
			return (null, "Path of the map file is not valid.");
		}
		catch (FileFormatException)
		{
			return (null, "The file you selected is empty and does not contain a valid PWSandbox map.");
		}
		catch (Exception ex) when (ex is FormatException or NotSupportedException)
		{
			return (null, $"""
				Map file is not valid!
				It's either made for a newer version of PWSandbox or just written incorrectly.

				Contact map maker and let them know about this problem.
				(If you are the map maker and map file is being loaded with the right version of PWSandbox,
				then you wrote map file in a wrong way. Check detailed message.)

				Detailed message: "{ex.Message}"
				""");
		}

		Dictionary<MapObject, Color>? colors = colorTheme switch
		{
			Theme.SimpleDark => new()
			{
				{ MapObject.Void, Color.Black },
				{ MapObject.Wall, Color.White },
				{ MapObject.FakeWall, Color.White },
				{ MapObject.Barrier, Color.Black }
			},
			_ => null
		};

		return (new PlayForm(mapObjects, colors), null);
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
