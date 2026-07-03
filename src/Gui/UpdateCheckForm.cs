// https://pws.yarb00.dev

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PWSandbox.Gui;

internal sealed partial class UpdateCheckForm : Form
{
	// csharpier-ignore
	private string
		latestVersion = string.Empty,
		updateUrl = string.Empty,
		errorMessage = string.Empty;

	// csharpier-ignore
	private StringId
		updateCheckStatus = StringId.CheckingForUpdatesStatus,
		updateCheckDetails = StringId.CheckingForUpdatesStatusDetails;

	public UpdateCheckForm()
	{
		InitializeComponent();

		ApplyLocalization();
		Localization.LocalizationChanged += (_, _) => ApplyLocalization();
	}

	private void ViewUpdate(object sender, EventArgs e) =>
		Process.Start(new ProcessStartInfo(updateUrl) { UseShellExecute = true });

	private async void CheckForUpdates(object sender, EventArgs e)
	{
		bool isUpdateAvailable;
		Version latestVersion;

		try
		{
			UpdateData updateData = await Updater.GetUpdateData();
			isUpdateAvailable = Updater.IsUpdateAvailable(updateData) ?? false;
			latestVersion =
				updateData.LatestVersion
				?? throw new FormatException(
					"The update data sent by server is not valid (version field is not found)."
				);
			this.latestVersion = latestVersion.ToString(3);
			updateUrl = updateData.DetailsUrl?.ToString() ?? string.Empty;
		}
		catch (Exception exception)
		{
			updateCheckStatus = StringId.ErrorStatus;
			updateCheckDetails = StringId.ErrorStatusDetails;

			errorMessage = exception.ToString();

			ApplyLocalization();

			return;
		}

		if (isUpdateAvailable)
		{
			latestVersionLabel.Enabled = viewUpdateButton.Enabled = true;
			latestVersionLabel.Visible = viewUpdateButton.Visible = true;

			updateCheckStatus = StringId.UpdateAvailableStatus;
			updateCheckDetails = StringId.UpdateAvailableStatusDetails;
		}
		else
		{
			updateCheckStatus = StringId.NoUpdateAvailableStatus;
			updateCheckDetails = StringId.NoUpdateAvailableStatusDetails;
		}

		ApplyLocalization();
	}

	private void ApplyLocalization()
	{
		RightToLeft = Localization.AreStringsRightToLeft ? RightToLeft.Yes : RightToLeft.No;
		RightToLeftLayout = Localization.AreStringsRightToLeft;

		Text = Localization.StringById[StringId.UpdateCheckTitle];
		updateCheckStatusLabel.Text = Localization.StringById[updateCheckStatus];
		updateCheckDetailsLabel.Text = Localization.StringById[StringId.DetailsSection];
		updateCheckDetailsRichTextBox.Text = Localization
			.StringById[updateCheckDetails]
			.Replace("\\(ERROR_MESSAGE)", errorMessage);
		latestVersionLabel.Text = Localization
			.StringById[StringId.LatestVersionInfoText]
			.Replace("\\(VERSION)", latestVersion);
		currentVersionLabel.Text = Localization
			.StringById[StringId.CurrentVersionInfoText]
			.Replace("\\(VERSION)", Program.FriendlyVersion);
		viewUpdateButton.Text = Localization.StringById[StringId.SeeUpdateAction];
		closeButton.Text = Localization.StringById[StringId.CloseAction];
	}
}
