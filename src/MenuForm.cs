// PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// Licensed under the MIT (Expat) license; Copyright (c) 2024-2025 yarb00

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWSandbox;

enum Theme
{
	SimpleDark,
	SimpleLight
}

enum ControlColor
{
	Background, Foreground,
	TextBoxBackground, TextBoxForeground,
	ButtonBackground, ButtonForeground,
	ButtonHovered, ButtonClicked
}

partial class MenuForm : Form
{
	#region GUI colors

	private static readonly Dictionary<ControlColor, Color> GuiColor_SimpleDark = new()
	{
		[ControlColor.Background] = Color.Black,
		[ControlColor.Foreground] = Color.White,
		[ControlColor.TextBoxBackground] = Color.FromArgb(64, 64, 64),
		[ControlColor.TextBoxForeground] = Color.White,
		[ControlColor.ButtonBackground] = Color.FromArgb(64, 64, 64),
		[ControlColor.ButtonForeground] = Color.White,
		[ControlColor.ButtonHovered] = Color.Gray,
		[ControlColor.ButtonClicked] = Color.DimGray
	};

	private static readonly Dictionary<ControlColor, Color> GuiColor_SimpleLight = new()
	{
		[ControlColor.Background] = Color.White,
		[ControlColor.Foreground] = Color.Black,
		[ControlColor.TextBoxBackground] = Color.Silver,
		[ControlColor.TextBoxForeground] = Color.Black,
		[ControlColor.ButtonBackground] = Color.Silver,
		[ControlColor.ButtonForeground] = Color.Black,
		[ControlColor.ButtonHovered] = Color.Gray,
		[ControlColor.ButtonClicked] = Color.FromArgb(64, 64, 64)
	};

	public static readonly Dictionary<Theme, Dictionary<ControlColor, Color>> GuiColor = new()
	{
		[Theme.SimpleDark] = GuiColor_SimpleDark,
		[Theme.SimpleLight] = GuiColor_SimpleLight
	};

	#endregion

	private static Theme currentColorTheme = Theme.SimpleDark;

	public MenuForm()
	{
		InitializeComponent();

		if (Program.Version is not null) Text += $" v{Program.Version.ToString(3)}";

		Task.Run(static async () =>
		{
			(bool isUpdateAvailable, Version latestVersion, string releaseUrl) updateInfo;

			try
			{
				updateInfo = await Updater.GetUpdateInfo();
			}
			catch
			{
				return;
			}

			if (updateInfo.isUpdateAvailable) new UpdateCheckForm(updateInfo).ShowDialog();
		});
	}

	#region Map loading

	private void LoadMapFile(object sender, EventArgs e)
	{
		if (mapOpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
		(PlayForm? playForm, string? errorText) = GetLoadedPlayForm(mapOpenFileDialog.FileName, currentColorTheme);

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
		catch (NotSupportedException ex) when (ex.Message.Contains("XML"))
		{
			return (null, $"""
				The map you are trying to open is made using the new XML based standard (2.x).

				You are running the legacy (WinForms based) version of PWSandbox,
				which doesn't support the new map standard.

				To be able to run the new XML-based maps, and receive new features and bug fixes,
				please upgrade to the latest cross platform version of PWSandbox.
				""");
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
			Theme.SimpleLight => null,
			_ => throw new NotImplementedException()
		};

		return (new PlayForm(mapObjects, colors), null);
	}

	#endregion

	private void SwitchTheme(object sender, EventArgs e)
	{
		currentColorTheme = currentColorTheme switch
		{
			Theme.SimpleLight => Theme.SimpleDark,
			Theme.SimpleDark => Theme.SimpleLight,
			_ => throw new NotImplementedException()
		};

		SetTheme(currentColorTheme);
	}

	private void SetTheme(Theme colorTheme)
	{
		var GuiColor = MenuForm.GuiColor[colorTheme];

		BackColor = GuiColor[ControlColor.Background];
		ForeColor = GuiColor[ControlColor.Foreground];
		foreach (Control control in Controls)
		{
			if (control is not Button button) continue;

			button.BackColor = GuiColor[ControlColor.ButtonBackground];
			button.ForeColor = GuiColor[ControlColor.ButtonForeground];
			button.FlatAppearance.MouseOverBackColor = GuiColor[ControlColor.ButtonHovered];
			button.FlatAppearance.MouseDownBackColor = GuiColor[ControlColor.ButtonClicked];
		}
	}

	private void OpenAboutAppDialog(object sender, EventArgs e) => new AboutForm(currentColorTheme).ShowDialog();
	private void CheckForUpdates(object sender, EventArgs e) => new UpdateCheckForm(null, currentColorTheme).ShowDialog();
}
