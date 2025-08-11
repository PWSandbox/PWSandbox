// PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// Licensed under the MIT (Expat) license; Copyright (c) 2024-2025 yarb00

using Octokit;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Forms;

namespace PWSandbox;

partial class UpdateCheckForm : Form
{
	private (bool isUpdateAvailable, Version latestVersion, string releaseUrl)? updateInfo;

	public UpdateCheckForm((bool isUpdateAvailable, Version latestVersion, string releaseUrl)? updateInfo = null,
		Theme colorTheme = Theme.SimpleDark)
	{
		InitializeComponent();

		if (Program.Version is null) appVersionLabel.Text = "Installed version: Unknown!";
		else appVersionLabel.Text = $"Installed version: {Program.Version.ToString(3)}";

		this.updateInfo = updateInfo;
		SetTheme(colorTheme);
	}

	private void SetTheme(Theme colorTheme)
	{
		var GuiColor = MenuForm.GuiColor[colorTheme];

		BackColor = GuiColor[ControlColor.Background];
		ForeColor = GuiColor[ControlColor.Foreground];

		updateDetailsRichTextBox.BackColor = GuiColor[ControlColor.TextBoxBackground];
		updateDetailsRichTextBox.ForeColor = GuiColor[ControlColor.TextBoxForeground];

		foreach (Control control in Controls)
		{
			if (control is not Button button) continue;

			button.BackColor = GuiColor[ControlColor.ButtonBackground];
			button.ForeColor = GuiColor[ControlColor.ButtonForeground];
			button.FlatAppearance.MouseOverBackColor = GuiColor[ControlColor.ButtonHovered];
			button.FlatAppearance.MouseDownBackColor = GuiColor[ControlColor.ButtonClicked];
		}
	}

	private async void CheckForUpdates(object sender, EventArgs e)
	{
		if (Program.Version is null)
		{
			updateStatusLabel.Text = "Error!";
			updateDetailsRichTextBox.Text = """
				An error occurred while checking for updates:
				Current version of this PWSandbox executable is unknown (possibly corrupted!), so it's impossible to check for updates.
				""";

			return;
		}

		bool isUpdateAvailable;
		Version latestVersion;
		string updateUrl;

		if (updateInfo is not null) (isUpdateAvailable, latestVersion, updateUrl) = ((bool, Version, string))updateInfo;
		else
		{
			updateStatusLabel.Text = "Checking for updates...";
			updateDetailsRichTextBox.Text = "Please wait... Fetching the latest data from the GitHub Releases API...";

			try
			{
				(isUpdateAvailable, latestVersion, updateUrl) = await Updater.GetUpdateInfo();
			}
			catch (HttpRequestException)
			{
				updateStatusLabel.Text = "Error!";
				updateDetailsRichTextBox.Text = """
				An error occurred while checking for updates:
				Failed to retrieve information from GitHub Releases. Check your internet connection and try again later.
				""";

				return;
			}
			catch (RateLimitExceededException)
			{
				updateStatusLabel.Text = "Error!";
				updateDetailsRichTextBox.Text = """
				Whoa, slow down!
				You have exceeded the rate limit and the GitHub API has temporarily blocked you.
				Wait for a few hours and try again.
				""";

				return;
			}
			catch (Exception ex)
			{
				string
					exceptionMessage = ex.Message,
					innerExceptionMessage = ex.InnerException?.Message ?? "[Not present.]",
					exceptionDetails = ex.StackTrace ?? "[Not present.]",
					innerExceptionDetails = ex.InnerException?.StackTrace ?? "[Not present.]";

				updateStatusLabel.Text = "Error!";
				updateDetailsRichTextBox.Text = $"""
				An error occurred while checking for updates.

				========== Details: ==========

				Message (inner exception): {innerExceptionMessage}
				Message: {exceptionMessage}
				
				----- Stack trace (inner exception): -----
				{innerExceptionDetails}
				
				----- Stack trace: -----
				{exceptionDetails}
				""";

				return;
			}
		}

		if (isUpdateAvailable)
		{
			newVersionLabel.Text = $"Latest version: {latestVersion.ToString(3)})";

			newVersionLabel.Enabled = true;
			viewUpdateButton.Enabled = true;
			newVersionLabel.Visible = true;
			viewUpdateButton.Visible = true;

			updateStatusLabel.Text = "An update is available!";
			updateDetailsRichTextBox.Text = $"To learn more info about the new PWSandbox v{latestVersion.ToString(3)} and download it, press the \"View update\" button below.";
		}
		else
		{
			updateStatusLabel.Text = "No updates!";
			updateDetailsRichTextBox.Text = "Congratulations! You're using the most recent version.";
		}
	}

	private void ViewUpdate(object sender, EventArgs e)
	{
		if (updateInfo is null) return;

		string url = (((bool, Version, string))updateInfo).Item3;
		Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
	}
}
