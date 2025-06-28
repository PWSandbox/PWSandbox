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
		okButton = new System.Windows.Forms.Button();
		appDescriptionRichTextBox = new System.Windows.Forms.RichTextBox();
		copyrightLabel = new System.Windows.Forms.Label();
		appVersionLabel = new System.Windows.Forms.Label();
		appNameLabel = new System.Windows.Forms.Label();
		appDescriptionLabel = new System.Windows.Forms.Label();
		appLicenseLabel = new System.Windows.Forms.Label();
		appLicenseRichTextBox = new System.Windows.Forms.RichTextBox();
		SuspendLayout();
		//
		// okButton
		//
		okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		okButton.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		okButton.FlatAppearance.BorderSize = 0;
		okButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
		okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		okButton.Location = new System.Drawing.Point(266, 366);
		okButton.Name = "okButton";
		okButton.Size = new System.Drawing.Size(88, 27);
		okButton.TabIndex = 7;
		okButton.Text = "&OK";
		okButton.UseVisualStyleBackColor = false;
		//
		// appDescriptionRichTextBox
		//
		appDescriptionRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appDescriptionRichTextBox.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		appDescriptionRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		appDescriptionRichTextBox.ForeColor = System.Drawing.Color.White;
		appDescriptionRichTextBox.Location = new System.Drawing.Point(12, 138);
		appDescriptionRichTextBox.Name = "appDescriptionRichTextBox";
		appDescriptionRichTextBox.ReadOnly = true;
		appDescriptionRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		appDescriptionRichTextBox.Size = new System.Drawing.Size(342, 76);
		appDescriptionRichTextBox.TabIndex = 3;
		appDescriptionRichTextBox.TabStop = false;
		appDescriptionRichTextBox.Text = "";
		appDescriptionRichTextBox.LinkClicked += OnLinkClick;
		//
		// copyrightLabel
		//
		copyrightLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		copyrightLabel.Location = new System.Drawing.Point(12, 79);
		copyrightLabel.Name = "copyrightLabel";
		copyrightLabel.Size = new System.Drawing.Size(342, 14);
		copyrightLabel.TabIndex = 1;
		copyrightLabel.Text = "Copyright (c) 2024-2025 yarb00";
		copyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		//
		// appVersionLabel
		//
		appVersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appVersionLabel.Location = new System.Drawing.Point(12, 369);
		appVersionLabel.Name = "appVersionLabel";
		appVersionLabel.Size = new System.Drawing.Size(248, 20);
		appVersionLabel.TabIndex = 6;
		appVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		//
		// appNameLabel
		//
		appNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appNameLabel.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		appNameLabel.Location = new System.Drawing.Point(12, 9);
		appNameLabel.Name = "appNameLabel";
		appNameLabel.Size = new System.Drawing.Size(342, 56);
		appNameLabel.TabIndex = 0;
		appNameLabel.Text = "PWSandbox";
		appNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		//
		// appDescriptionLabel
		//
		appDescriptionLabel.AutoSize = true;
		appDescriptionLabel.Location = new System.Drawing.Point(12, 121);
		appDescriptionLabel.Name = "appDescriptionLabel";
		appDescriptionLabel.Size = new System.Drawing.Size(91, 14);
		appDescriptionLabel.TabIndex = 2;
		appDescriptionLabel.Text = "Description:";
		appDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		//
		// appLicenseLabel
		//
		appLicenseLabel.AutoSize = true;
		appLicenseLabel.Location = new System.Drawing.Point(12, 245);
		appLicenseLabel.Name = "appLicenseLabel";
		appLicenseLabel.Size = new System.Drawing.Size(63, 14);
		appLicenseLabel.TabIndex = 4;
		appLicenseLabel.Text = "License:";
		appLicenseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		//
		// appLicenseRichTextBox
		//
		appLicenseRichTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appLicenseRichTextBox.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		appLicenseRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		appLicenseRichTextBox.ForeColor = System.Drawing.Color.White;
		appLicenseRichTextBox.Location = new System.Drawing.Point(12, 262);
		appLicenseRichTextBox.Name = "appLicenseRichTextBox";
		appLicenseRichTextBox.ReadOnly = true;
		appLicenseRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		appLicenseRichTextBox.Size = new System.Drawing.Size(342, 76);
		appLicenseRichTextBox.TabIndex = 5;
		appLicenseRichTextBox.TabStop = false;
		appLicenseRichTextBox.Text = "";
		//
		// AboutForm
		//
		AcceptButton = okButton;
		AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		BackColor = System.Drawing.Color.Black;
		CancelButton = okButton;
		ClientSize = new System.Drawing.Size(366, 401);
		Controls.Add(appVersionLabel);
		Controls.Add(okButton);
		Controls.Add(appDescriptionRichTextBox);
		Controls.Add(appLicenseRichTextBox);
		Controls.Add(appLicenseLabel);
		Controls.Add(appDescriptionLabel);
		Controls.Add(copyrightLabel);
		Controls.Add(appNameLabel);
		Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		ForeColor = System.Drawing.Color.White;
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		MinimumSize = new System.Drawing.Size(352, 440);
		Name = "AboutForm";
		ShowIcon = false;
		ShowInTaskbar = false;
		StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		Text = "About PWSandbox";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.RichTextBox appDescriptionRichTextBox;
	private System.Windows.Forms.Label copyrightLabel;
	private System.Windows.Forms.Label appVersionLabel;
	private System.Windows.Forms.Label appNameLabel;
	private System.Windows.Forms.Label appDescriptionLabel;
	private System.Windows.Forms.Label appLicenseLabel;
	private System.Windows.Forms.RichTextBox appLicenseRichTextBox;
}
