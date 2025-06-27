// This file is a part of PWSandbox ( https://github.com/yarb00/PWSandbox )
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

partial class UpdateCheckForm
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
		updateStatusLabel = new System.Windows.Forms.Label();
		appVersionLabel = new System.Windows.Forms.Label();
		cancelButton = new System.Windows.Forms.Button();
		viewUpdateButton = new System.Windows.Forms.Button();
		newVersionLabel = new System.Windows.Forms.Label();
		updateDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
		updateDetailsLabel = new System.Windows.Forms.Label();
		SuspendLayout();
		//
		// updateStatusLabel
		//
		updateStatusLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		updateStatusLabel.Location = new System.Drawing.Point(12, 9);
		updateStatusLabel.Name = "updateStatusLabel";
		updateStatusLabel.Size = new System.Drawing.Size(440, 42);
		updateStatusLabel.TabIndex = 0;
		updateStatusLabel.Text = "Update status";
		updateStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		//
		// appVersionLabel
		//
		appVersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appVersionLabel.Location = new System.Drawing.Point(12, 285);
		appVersionLabel.Name = "appVersionLabel";
		appVersionLabel.Size = new System.Drawing.Size(340, 20);
		appVersionLabel.TabIndex = 5;
		appVersionLabel.Text = "Current installed version: ?";
		appVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		//
		// cancelButton
		//
		cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		cancelButton.Location = new System.Drawing.Point(358, 282);
		cancelButton.Name = "cancelButton";
		cancelButton.Size = new System.Drawing.Size(94, 27);
		cancelButton.TabIndex = 4;
		cancelButton.Text = "Cancel";
		//
		// viewUpdateButton
		//
		viewUpdateButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		viewUpdateButton.Enabled = false;
		viewUpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		viewUpdateButton.Location = new System.Drawing.Point(358, 249);
		viewUpdateButton.Name = "viewUpdateButton";
		viewUpdateButton.Size = new System.Drawing.Size(94, 27);
		viewUpdateButton.TabIndex = 6;
		viewUpdateButton.Text = "View update";
		viewUpdateButton.Visible = false;
		viewUpdateButton.Click += ViewUpdate;
		//
		// newVersionLabel
		//
		newVersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		newVersionLabel.Enabled = false;
		newVersionLabel.Location = new System.Drawing.Point(12, 252);
		newVersionLabel.Name = "newVersionLabel";
		newVersionLabel.Size = new System.Drawing.Size(340, 20);
		newVersionLabel.TabIndex = 7;
		newVersionLabel.Text = "Newer version available! (Version ?)";
		newVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		newVersionLabel.Visible = false;
		//
		// updateDetailsRichTextBox
		//
		updateDetailsRichTextBox.BackColor = System.Drawing.Color.Black;
		updateDetailsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		updateDetailsRichTextBox.ForeColor = System.Drawing.Color.White;
		updateDetailsRichTextBox.Location = new System.Drawing.Point(12, 96);
		updateDetailsRichTextBox.Name = "updateDetailsRichTextBox";
		updateDetailsRichTextBox.ReadOnly = true;
		updateDetailsRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		updateDetailsRichTextBox.Size = new System.Drawing.Size(440, 147);
		updateDetailsRichTextBox.TabIndex = 8;
		updateDetailsRichTextBox.TabStop = false;
		updateDetailsRichTextBox.Text = "";
		//
		// updateDetailsLabel
		//
		updateDetailsLabel.AutoSize = true;
		updateDetailsLabel.Location = new System.Drawing.Point(12, 79);
		updateDetailsLabel.Name = "updateDetailsLabel";
		updateDetailsLabel.Size = new System.Drawing.Size(63, 14);
		updateDetailsLabel.TabIndex = 11;
		updateDetailsLabel.Text = "Details:";
		updateDetailsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		//
		// UpdateCheckForm
		//
		AcceptButton = viewUpdateButton;
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		BackColor = System.Drawing.Color.Black;
		CancelButton = cancelButton;
		ClientSize = new System.Drawing.Size(464, 321);
		Controls.Add(updateDetailsRichTextBox);
		Controls.Add(updateDetailsLabel);
		Controls.Add(newVersionLabel);
		Controls.Add(viewUpdateButton);
		Controls.Add(appVersionLabel);
		Controls.Add(cancelButton);
		Controls.Add(updateStatusLabel);
		Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		ForeColor = System.Drawing.Color.White;
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "UpdateCheckForm";
		StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		Text = "PWSandbox Updater";
		Load += CheckForUpdates;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.Label updateStatusLabel;
	private System.Windows.Forms.Label appVersionLabel;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button viewUpdateButton;
	private System.Windows.Forms.Label newVersionLabel;
	private System.Windows.Forms.RichTextBox updateDetailsRichTextBox;
	private System.Windows.Forms.Label updateDetailsLabel;
}
