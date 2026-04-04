// https://pws.yarb00.dev

using System.Diagnostics;
using System.Windows.Forms;

namespace PWSandbox;

internal sealed partial class AboutForm : Form
{
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
			.Replace("\\(VERSION)", Program.FriendlyVersion)
			.Replace("\\(BUILD_TYPE)", Program.BuildType)
#if NATIVEAOT
			.Replace("\\(COMPILATION_TYPE)", "NativeAOT");
#else
			.Replace("\\(COMPILATION_TYPE)", "JIT");
#endif
		okButton.Text = Localization.StringById[StringId.OkAction];
	}
}
