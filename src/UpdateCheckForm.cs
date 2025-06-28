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

using Octokit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWSandbox;

public partial class UpdateCheckForm : Form
{
	private string? updateUrl = null;
	private readonly Version? currentVersion;

	public UpdateCheckForm(MenuForm? menuForm = null)
	{
		InitializeComponent();

		currentVersion = menuForm?.AppVersion;

		if (currentVersion is null) appVersionLabel.Text = "Current installed version: Missing version!";
		else appVersionLabel.Text = $"Current installed version: v{currentVersion.ToString(3)}";

		SetTheme(menuForm?.CurrentColorTheme ?? Theme.SimpleDark);
	}

	private void SetTheme(Theme colorTheme)
	{
		var GuiColor = MenuForm.GuiColor[colorTheme];

		BackColor = GuiColor[ControlColor.Background];
		ForeColor = GuiColor[ControlColor.Foreground];

		updateDetailsRichTextBox.BackColor = GuiColor[ControlColor.ButtonBackground];
		updateDetailsRichTextBox.ForeColor = GuiColor[ControlColor.ButtonForeground];

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
		if (currentVersion is null)
		{
			updateStatusLabel.Text = "Error!";
			updateDetailsRichTextBox.Text = """
				An error occured while checking for updates:
				Current version of this PWSandbox executable is unknown (possibly corrupted), so it's impossible to check for updates.
				""";

			return;
		}

		updateStatusLabel.Text = "Checking for updates...";
		updateDetailsRichTextBox.Text = "Please wait... Fetching the latest data from the GitHub Releases API...";

		bool isUpdateAvailable;
		Version latestVersion;

		try
		{
			(isUpdateAvailable, latestVersion, updateUrl) = await GetUpdateInfo(currentVersion);
		}
		catch (HttpRequestException)
		{
			updateStatusLabel.Text = "Error!";
			updateDetailsRichTextBox.Text = """
				An error occured while checking for updates:
				Failed to retrieve information from GitHub Releases.
				Check your internet connection and try again later.
				""";

			return;
		}
		catch (RateLimitExceededException)
		{
			updateStatusLabel.Text = "Error!";
			updateDetailsRichTextBox.Text = """
				An error occured while checking for updates:
				Whoa, slow down!
				You have exceeded the rate limit and the GitHub API has temporarily blocked you.
				Wait about an hour and try again.
				""";

			return;
		}

		if (isUpdateAvailable)
		{
			newVersionLabel.Text = $"Newer version available! (Version {latestVersion.ToString(3)})";

			newVersionLabel.Enabled = true;
			viewUpdateButton.Enabled = true;
			newVersionLabel.Visible = true;
			viewUpdateButton.Visible = true;

			updateStatusLabel.Text = "An update is available!";
			updateDetailsRichTextBox.Text = $"To view and download the {latestVersion.ToString(3)} update, press the \"View update\" button below.";
		}
		else
		{
			updateStatusLabel.Text = "No updates!";
			updateDetailsRichTextBox.Text = "Congratulations! You're using the most recent version.";
		}
	}

	private void ViewUpdate(object sender, EventArgs e)
	{
		if (updateUrl is null) return;
		System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(updateUrl) { UseShellExecute = true });
	}

	private static async Task<(bool isUpdateAvailable, Version latestVersion, string releaseUrl)> GetUpdateInfo(Version version)
	{
		GitHubClient githubClient = new(new ProductHeaderValue("PWSandbox", version.ToString(3)));

		IReadOnlyList<Release> releases;

		try
		{
			releases = await githubClient.Repository.Release.GetAll("PWSandbox", "PWSandbox");
		}
		catch (Exception)
		{
			throw;
		}

		Release latestRelease = releases[0];

		if (!latestRelease.TagName.StartsWith('v'))
			throw new FormatException("Tag name doesn't begin with \"v\"");

		Version
			fetchedVersion = Version.Parse(latestRelease.TagName.AsSpan(1)),

			latestVersion = new(
				fetchedVersion.Major,
				fetchedVersion.Minor,
				fetchedVersion.Build,
				0
			),

			currentVersion = new(
				version.Major,
				version.Minor,
				version.Build,
				0
			);

		if (currentVersion.CompareTo(latestVersion) < 0) // if version on github is newer
			return (true, latestVersion, latestRelease.HtmlUrl);
		else
			return (false, latestVersion, latestRelease.HtmlUrl);
	}
}
