// https://pws.yarb00.dev

using System;
using System.Windows.Forms;

namespace PWSandbox;

internal sealed partial class MenuForm : Form
{
	public MenuForm()
	{
		InitializeComponent();
		Text += $" v{Program.FriendlyVersion}";

		ApplyLocalization();
		Localization.LocalizationChanged += (_, _) => ApplyLocalization();

#if NATIVE_AOT
		foreach (Language language in Localization.AvailableLanguages) languageComboBox.Items.Add(language);
		languageComboBox.SelectedIndex = 0;
#else
		languageComboBox.DataSource = Localization.AvailableLanguages;
#endif
	}

	private void LoadMap(object sender, EventArgs e)
	{
		if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

		Map map;
		try
		{
			map = MapParser.ParseMapFromFile(openFileDialog.FileName);
		}
		catch (Exception ex) when (ex is FormatException or NotSupportedException)
		{
			MessageBox.Show(
				this,
				$"""
				{Localization.StringById[StringId.MapFileErrorText]}

				{Localization.StringById[StringId.DetailsSection]}
				{ex.Message}
				""",
				Localization.StringById[StringId.MapFileErrorTitle],
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1,
				Localization.AreStringsRightToLeft ? MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading : 0
			);

			return;
		}

		new PlayForm(map).Show();
	}

	private void OpenAboutAppDialog(object sender, EventArgs e) => new AboutForm().ShowDialog(this);

	private void OpenUpdaterDialog(object sender, EventArgs e) => new UpdateCheckForm().ShowDialog(this);

	private void ChangeLanguage(object sender, EventArgs e) => Localization.SetLanguage(((Language)languageComboBox.SelectedItem!).LanguageId);

	private void ApplyLocalization()
	{
		RightToLeft = Localization.AreStringsRightToLeft ? RightToLeft.Yes : RightToLeft.No;
		RightToLeftLayout = Localization.AreStringsRightToLeft;

		loadMapButton.Text = Localization.StringById[StringId.LoadMapAction];
		aboutAppButton.Text = Localization.StringById[StringId.AboutAppAction];
		checkForUpdatesButton.Text = Localization.StringById[StringId.CheckForUpdatesAction];
		languageLabel.Text = Localization.StringById[StringId.LanguageSection];
		openFileDialog.Title = Localization.StringById[StringId.MapFileSelectionTitle];
	}
}
