// PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// Licensed under the MIT (Expat) license; Copyright (c) 2024-2025 yarb00

using System.Diagnostics;
using System.Windows.Forms;

namespace PWSandbox;

partial class AboutForm : Form
{
	public AboutForm(Theme colorTheme = Theme.SimpleDark)
	{
		InitializeComponent();

		if (Program.Version is not null) appVersionLabel.Text = $"v{Program.Version.ToString(3)}";

		appDescriptionRichTextBox.Text = """
			Simple sandbox game, built with .NET and Windows Forms.

			You can find the PWSandbox Git repository at https://github.com/PWSandbox/PWSandbox.

			This software is licensed under the MIT (Expat) License. You can find its text below.
			""";

		appLicenseRichTextBox.Text = """
			Permission is hereby granted, free of charge, to any person obtaining a copy
			of this software and associated documentation files (the ""Software""), to deal
			in the Software without restriction, including without limitation the rights
			to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
			copies of the Software, and to permit persons to whom the Software is
			furnished to do so, subject to the following conditions:

			The above copyright notice and this permission notice shall be included in all
			copies or substantial portions of the Software.

			THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
			IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
			FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
			AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
			LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
			OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
			SOFTWARE.
			""";

		SetTheme(colorTheme);
	}

	private void OnLinkClick(object sender, LinkClickedEventArgs e) => Process.Start(new ProcessStartInfo(e.LinkText!) { UseShellExecute = true });

	private void SetTheme(Theme colorTheme)
	{
		var GuiColor = MenuForm.GuiColor[colorTheme];

		BackColor = GuiColor[ControlColor.Background];
		ForeColor = GuiColor[ControlColor.Foreground];

		appDescriptionRichTextBox.BackColor =
		appLicenseRichTextBox.BackColor =
		GuiColor[ControlColor.TextBoxBackground];

		appDescriptionRichTextBox.ForeColor =
		appLicenseRichTextBox.ForeColor =
		GuiColor[ControlColor.TextBoxForeground];

		okButton.BackColor = GuiColor[ControlColor.ButtonBackground];
		okButton.ForeColor = GuiColor[ControlColor.ButtonForeground];

		okButton.FlatAppearance.MouseOverBackColor = GuiColor[ControlColor.ButtonHovered];
		okButton.FlatAppearance.MouseDownBackColor = GuiColor[ControlColor.ButtonClicked];
	}
}
