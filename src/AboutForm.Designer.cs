// https://pws.yarb00.dev

namespace PWSandbox;

partial class AboutForm
{
	private System.ComponentModel.IContainer components = null;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components is not null) components.Dispose();
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	private void InitializeComponent()
	{
		tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
		appNameLabel = new System.Windows.Forms.Label();
		descriptionLabel = new System.Windows.Forms.Label();
		descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
		licenseLabel = new System.Windows.Forms.Label();
		licenseRichTextBox = new System.Windows.Forms.RichTextBox();
		appVersionLabel = new System.Windows.Forms.Label();
		okButton = new System.Windows.Forms.Button();
		tableLayoutPanel.SuspendLayout();
		SuspendLayout();
		// 
		// tableLayoutPanel
		// 
		tableLayoutPanel.AutoSize = true;
		tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		tableLayoutPanel.ColumnCount = 1;
		tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel.Controls.Add(appNameLabel, 0, 0);
		tableLayoutPanel.Controls.Add(descriptionLabel, 0, 1);
		tableLayoutPanel.Controls.Add(descriptionRichTextBox, 0, 2);
		tableLayoutPanel.Controls.Add(licenseLabel, 0, 3);
		tableLayoutPanel.Controls.Add(licenseRichTextBox, 0, 4);
		tableLayoutPanel.Controls.Add(appVersionLabel, 0, 5);
		tableLayoutPanel.Controls.Add(okButton, 0, 6);
		tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
		tableLayoutPanel.Name = "tableLayoutPanel";
		tableLayoutPanel.RowCount = 7;
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
		tableLayoutPanel.Size = new System.Drawing.Size(264, 321);
		tableLayoutPanel.TabIndex = 0;
		// 
		// appNameLabel
		// 
		appNameLabel.AutoSize = true;
		appNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		appNameLabel.Font = new System.Drawing.Font("Consolas", 36F, System.Drawing.FontStyle.Bold);
		appNameLabel.Location = new System.Drawing.Point(3, 0);
		appNameLabel.Name = "appNameLabel";
		appNameLabel.Size = new System.Drawing.Size(258, 64);
		appNameLabel.TabIndex = 1;
		appNameLabel.Text = "PWSandbox";
		appNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// descriptionLabel
		// 
		descriptionLabel.AutoSize = true;
		descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		descriptionLabel.Location = new System.Drawing.Point(3, 64);
		descriptionLabel.Name = "descriptionLabel";
		descriptionLabel.Size = new System.Drawing.Size(258, 16);
		descriptionLabel.TabIndex = 2;
		descriptionLabel.Text = "DescriptionSection";
		descriptionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		// 
		// descriptionRichTextBox
		// 
		descriptionRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		descriptionRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
		descriptionRichTextBox.Location = new System.Drawing.Point(3, 83);
		descriptionRichTextBox.Name = "descriptionRichTextBox";
		descriptionRichTextBox.ReadOnly = true;
		descriptionRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
		descriptionRichTextBox.Size = new System.Drawing.Size(258, 74);
		descriptionRichTextBox.TabIndex = 3;
		descriptionRichTextBox.Text = "";
		descriptionRichTextBox.LinkClicked += OpenLink;
		// 
		// licenseLabel
		// 
		licenseLabel.AutoSize = true;
		licenseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		licenseLabel.Location = new System.Drawing.Point(3, 160);
		licenseLabel.Name = "licenseLabel";
		licenseLabel.Size = new System.Drawing.Size(258, 16);
		licenseLabel.TabIndex = 4;
		licenseLabel.Text = "LicenseSection";
		licenseLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		// 
		// licenseRichTextBox
		// 
		licenseRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		licenseRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
		licenseRichTextBox.Location = new System.Drawing.Point(3, 179);
		licenseRichTextBox.Name = "licenseRichTextBox";
		licenseRichTextBox.ReadOnly = true;
		licenseRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
		licenseRichTextBox.Size = new System.Drawing.Size(258, 74);
		licenseRichTextBox.TabIndex = 5;
		licenseRichTextBox.Text = "";
		// 
		// appVersionLabel
		// 
		appVersionLabel.AutoSize = true;
		appVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		appVersionLabel.Location = new System.Drawing.Point(3, 256);
		appVersionLabel.Name = "appVersionLabel";
		appVersionLabel.Size = new System.Drawing.Size(258, 32);
		appVersionLabel.TabIndex = 6;
		appVersionLabel.Text = "VersionText";
		appVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// okButton
		// 
		okButton.AutoSize = true;
		okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		okButton.Dock = System.Windows.Forms.DockStyle.Fill;
		okButton.Location = new System.Drawing.Point(3, 291);
		okButton.Name = "okButton";
		okButton.Size = new System.Drawing.Size(258, 27);
		okButton.TabIndex = 7;
		okButton.Text = "OkAction";
		okButton.UseVisualStyleBackColor = true;
		// 
		// AboutForm
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		AutoSize = true;
		AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		CancelButton = okButton;
		ClientSize = new System.Drawing.Size(264, 321);
		Controls.Add(tableLayoutPanel);
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimumSize = new System.Drawing.Size(280, 360);
		Name = "AboutForm";
		ShowIcon = false;
		Text = "AboutAppAction";
		tableLayoutPanel.ResumeLayout(false);
		tableLayoutPanel.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
	private System.Windows.Forms.Label appNameLabel;
	private System.Windows.Forms.Label descriptionLabel;
	private System.Windows.Forms.RichTextBox descriptionRichTextBox;
	private System.Windows.Forms.Label licenseLabel;
	private System.Windows.Forms.RichTextBox licenseRichTextBox;
	private System.Windows.Forms.Label appVersionLabel;
	private System.Windows.Forms.Button okButton;
}
