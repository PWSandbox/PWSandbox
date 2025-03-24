// This file is a part of PWSandbox ( https://github.com/yarb00/PWSandbox )
// PWSandbox is licensed under the MIT (X11) License:

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

using System.Drawing;
using System.Windows.Forms;

namespace PWSandbox;

partial class AboutForm
{
	private System.ComponentModel.IContainer components = null;

	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null)) components.Dispose();
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	private void InitializeComponent()
	{
		okButton = new Button();
		appDescriptionTextBox = new TextBox();
		copyrightLabel = new Label();
		appVersionLabel = new Label();
		appNameLabel = new Label();
		SuspendLayout();
		//
		// okButton
		//
		okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		okButton.DialogResult = DialogResult.Cancel;
		okButton.Location = new Point(240, 215);
		okButton.Name = "okButton";
		okButton.Size = new Size(88, 27);
		okButton.TabIndex = 0;
		okButton.Text = "&OK";
		//
		// appDescriptionTextBox
		//
		appDescriptionTextBox.Location = new Point(12, 62);
		appDescriptionTextBox.Multiline = true;
		appDescriptionTextBox.Name = "appDescriptionTextBox";
		appDescriptionTextBox.ReadOnly = true;
		appDescriptionTextBox.ScrollBars = ScrollBars.Vertical;
		appDescriptionTextBox.Size = new Size(316, 147);
		appDescriptionTextBox.TabIndex = 1;
		appDescriptionTextBox.TabStop = false;
		//
		// copyrightLabel
		//
		copyrightLabel.AutoSize = true;
		copyrightLabel.Location = new Point(12, 44);
		copyrightLabel.MaximumSize = new Size(0, 20);
		copyrightLabel.Name = "copyrightLabel";
		copyrightLabel.Size = new Size(171, 15);
		copyrightLabel.TabIndex = 2;
		copyrightLabel.Text = "Copyright (c) 2024-2025 yarb00";
		copyrightLabel.TextAlign = ContentAlignment.MiddleLeft;
		//
		// appVersionLabel
		//
		appVersionLabel.Location = new Point(12, 24);
		appVersionLabel.MaximumSize = new Size(0, 20);
		appVersionLabel.Name = "appVersionLabel";
		appVersionLabel.Size = new Size(316, 20);
		appVersionLabel.TabIndex = 3;
		appVersionLabel.Text = "Version ?";
		appVersionLabel.TextAlign = ContentAlignment.MiddleLeft;
		//
		// appNameLabel
		//
		appNameLabel.AutoSize = true;
		appNameLabel.Location = new Point(12, 9);
		appNameLabel.MaximumSize = new Size(0, 20);
		appNameLabel.Name = "appNameLabel";
		appNameLabel.Size = new Size(70, 15);
		appNameLabel.TabIndex = 4;
		appNameLabel.Text = "PWSandbox";
		appNameLabel.TextAlign = ContentAlignment.MiddleLeft;
		//
		// AboutForm
		//
		AcceptButton = okButton;
		AutoScaleMode = AutoScaleMode.None;
		ClientSize = new Size(336, 249);
		Controls.Add(okButton);
		Controls.Add(appDescriptionTextBox);
		Controls.Add(copyrightLabel);
		Controls.Add(appVersionLabel);
		Controls.Add(appNameLabel);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "AboutForm";
		ShowIcon = false;
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "About PWSandbox";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Button okButton;
	private TextBox appDescriptionTextBox;
	private Label copyrightLabel;
	private Label appVersionLabel;
	private Label appNameLabel;
}
