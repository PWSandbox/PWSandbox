// https://pws.yarb00.dev

using System.Diagnostics;
using System.Windows.Forms;

namespace PWSandbox;

internal sealed partial class AboutForm : Form
{
#if DEBUG
	private const string buildType = "Debug";
#else
	private const string buildType = "Release";
#endif

#if NATIVE_AOT
	private const string compilationType = "AOT";
#else
	private const string compilationType = "JIT";
#endif

	public AboutForm()
	{
		InitializeComponent();

		ApplyLocalization();
		Localization.LocalizationChanged += (_, _) => ApplyLocalization();
	}

	private void OpenLink(object sender, LinkClickedEventArgs e) => Process.Start(new ProcessStartInfo(e.LinkText!) { UseShellExecute = true });

	private void ApplyLocalization()
	{
		RightToLeft = Localization.AreStringsRightToLeft ? RightToLeft.Yes : RightToLeft.No;
		RightToLeftLayout = Localization.AreStringsRightToLeft;

		Text = Localization.StringById[StringId.AboutAppAction];
		descriptionLabel.Text = Localization.StringById[StringId.DescriptionSection];
		descriptionRichTextBox.Text = Localization.StringById[StringId.DescriptionText]
			.Replace("\\(WEBSITE_LINK)", Program.Website);
		licenseLabel.Text = Localization.StringById[StringId.LicenseSection];
		licenseRichTextBox.Text = Program.License; // Not localized on purpose
		appVersionLabel.Text = Localization.StringById[StringId.VersionText]
			.Replace("\\(VERSION)", Program.FullVersion)
			.Replace("\\(BUILD_TYPE)", buildType)
			.Replace("\\(COMPILATION_TYPE)", compilationType);
		okButton.Text = Localization.StringById[StringId.OkAction];
	}
}
