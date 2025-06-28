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

public enum ControlColor
{
	Background, Foreground,
	ButtonBackground, ButtonForeground,
	ButtonHovered, ButtonClicked
}

public partial class MenuForm : Form
{
	#region GUI colors

	private static readonly Dictionary<ControlColor, Color> GuiColor_SimpleDark = new()
	{
		{ ControlColor.Background, Color.Black },
		{ ControlColor.Foreground, Color.White },
		{ ControlColor.ButtonBackground, Color.FromArgb(64, 64, 64) },
		{ ControlColor.ButtonForeground, Color.White },
		{ ControlColor.ButtonHovered, Color.Gray },
		{ ControlColor.ButtonClicked, Color.DimGray }
	};

	private static readonly Dictionary<ControlColor, Color> GuiColor_SimpleLight = new()
	{
		{ ControlColor.Background, Color.White },
		{ ControlColor.Foreground, Color.Black },
		{ ControlColor.ButtonBackground, Color.Silver },
		{ ControlColor.ButtonForeground, Color.Black },
		{ ControlColor.ButtonHovered, Color.Gray },
		{ ControlColor.ButtonClicked, Color.FromArgb(64, 64, 64) }
	};

	public static readonly Dictionary<Theme, Dictionary<ControlColor, Color>> GuiColor = new()
	{
		{ Theme.SimpleDark, GuiColor_SimpleDark },
		{ Theme.SimpleLight, GuiColor_SimpleLight }
	};

	#endregion

	public Theme CurrentColorTheme { get; private set; } = Theme.SimpleDark;
	public readonly Version? AppVersion;

	public MenuForm()
	{
		InitializeComponent();

		AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
		if (AppVersion is not null) Text += $" v{AppVersion.ToString(3)}";
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
				{ MapObject.Wall, Color.FromArgb(64, 64, 64) },
				{ MapObject.FakeWall, Color.FromArgb(64, 64, 64) }, // Same color as Wall
			},
			_ => null
		};

		return (new PlayForm(mapObjects, colors), null);
	}

	#endregion

	private void SwitchTheme(object sender, EventArgs e)
	{
		CurrentColorTheme = CurrentColorTheme switch
		{
			Theme.SimpleLight => Theme.SimpleDark,
			Theme.SimpleDark => Theme.SimpleLight,
			_ => throw new NotImplementedException()
		};

		BackColor = GuiColor[CurrentColorTheme][ControlColor.Background];
		ForeColor = GuiColor[CurrentColorTheme][ControlColor.Foreground];
		foreach (Control control in Controls)
		{
			if (control is not Button button) continue;

			button.BackColor = GuiColor[CurrentColorTheme][ControlColor.ButtonBackground];
			button.ForeColor = GuiColor[CurrentColorTheme][ControlColor.ButtonForeground];
			button.FlatAppearance.MouseOverBackColor = GuiColor[CurrentColorTheme][ControlColor.ButtonHovered];
			button.FlatAppearance.MouseDownBackColor = GuiColor[CurrentColorTheme][ControlColor.ButtonClicked];
		}
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
