// https://pws.yarb00.dev

namespace PWSandbox;

partial class UpdateCheckForm
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
		updateCheckStatusLabel = new System.Windows.Forms.Label();
		updateCheckDetailsLabel = new System.Windows.Forms.Label();
		updateCheckDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
		latestVersionLabel = new System.Windows.Forms.Label();
		currentVersionLabel = new System.Windows.Forms.Label();
		viewUpdateButton = new System.Windows.Forms.Button();
		closeButton = new System.Windows.Forms.Button();
		tableLayoutPanel.SuspendLayout();
		SuspendLayout();
		// 
		// tableLayoutPanel
		// 
		tableLayoutPanel.AutoSize = true;
		tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		tableLayoutPanel.ColumnCount = 1;
		tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel.Controls.Add(updateCheckStatusLabel, 0, 0);
		tableLayoutPanel.Controls.Add(updateCheckDetailsLabel, 0, 1);
		tableLayoutPanel.Controls.Add(updateCheckDetailsRichTextBox, 0, 2);
		tableLayoutPanel.Controls.Add(latestVersionLabel, 0, 3);
		tableLayoutPanel.Controls.Add(currentVersionLabel, 0, 4);
		tableLayoutPanel.Controls.Add(viewUpdateButton, 0, 5);
		tableLayoutPanel.Controls.Add(closeButton, 0, 6);
		tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
		tableLayoutPanel.Name = "tableLayoutPanel";
		tableLayoutPanel.RowCount = 7;
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
		tableLayoutPanel.Size = new System.Drawing.Size(464, 321);
		tableLayoutPanel.TabIndex = 0;
		// 
		// updateCheckStatusLabel
		// 
		updateCheckStatusLabel.AutoSize = true;
		updateCheckStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		updateCheckStatusLabel.Font = new System.Drawing.Font("Segoe UI", 36F);
		updateCheckStatusLabel.Location = new System.Drawing.Point(3, 0);
		updateCheckStatusLabel.Name = "updateCheckStatusLabel";
		updateCheckStatusLabel.Size = new System.Drawing.Size(458, 64);
		updateCheckStatusLabel.TabIndex = 1;
		updateCheckStatusLabel.Text = "...";
		updateCheckStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// updateCheckDetailsLabel
		// 
		updateCheckDetailsLabel.AutoSize = true;
		updateCheckDetailsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		updateCheckDetailsLabel.Location = new System.Drawing.Point(3, 64);
		updateCheckDetailsLabel.Name = "updateCheckDetailsLabel";
		updateCheckDetailsLabel.Size = new System.Drawing.Size(458, 16);
		updateCheckDetailsLabel.TabIndex = 2;
		updateCheckDetailsLabel.Text = "DetailsSection";
		updateCheckDetailsLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		// 
		// updateCheckDetailsRichTextBox
		// 
		updateCheckDetailsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		updateCheckDetailsRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
		updateCheckDetailsRichTextBox.Location = new System.Drawing.Point(3, 83);
		updateCheckDetailsRichTextBox.Name = "updateCheckDetailsRichTextBox";
		updateCheckDetailsRichTextBox.ReadOnly = true;
		updateCheckDetailsRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
		updateCheckDetailsRichTextBox.Size = new System.Drawing.Size(458, 106);
		updateCheckDetailsRichTextBox.TabIndex = 3;
		updateCheckDetailsRichTextBox.Text = "";
		// 
		// latestVersionLabel
		// 
		latestVersionLabel.AutoSize = true;
		latestVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		latestVersionLabel.Enabled = false;
		latestVersionLabel.Location = new System.Drawing.Point(3, 192);
		latestVersionLabel.Name = "latestVersionLabel";
		latestVersionLabel.Size = new System.Drawing.Size(458, 32);
		latestVersionLabel.TabIndex = 4;
		latestVersionLabel.Text = "LatestVersionInfoText";
		latestVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		latestVersionLabel.Visible = false;
		// 
		// currentVersionLabel
		// 
		currentVersionLabel.AutoSize = true;
		currentVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		currentVersionLabel.Location = new System.Drawing.Point(3, 224);
		currentVersionLabel.Name = "currentVersionLabel";
		currentVersionLabel.Size = new System.Drawing.Size(458, 32);
		currentVersionLabel.TabIndex = 5;
		currentVersionLabel.Text = "CurrentVersionInfoText";
		currentVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		// 
		// viewUpdateButton
		// 
		viewUpdateButton.AutoSize = true;
		viewUpdateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		viewUpdateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		viewUpdateButton.Dock = System.Windows.Forms.DockStyle.Fill;
		viewUpdateButton.Enabled = false;
		viewUpdateButton.Location = new System.Drawing.Point(3, 259);
		viewUpdateButton.Name = "viewUpdateButton";
		viewUpdateButton.Size = new System.Drawing.Size(458, 26);
		viewUpdateButton.TabIndex = 6;
		viewUpdateButton.Text = "SeeUpdateAction";
		viewUpdateButton.UseVisualStyleBackColor = true;
		viewUpdateButton.Visible = false;
		viewUpdateButton.Click += ViewUpdate;
		// 
		// closeButton
		// 
		closeButton.AutoSize = true;
		closeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		closeButton.Dock = System.Windows.Forms.DockStyle.Fill;
		closeButton.Location = new System.Drawing.Point(3, 291);
		closeButton.Name = "closeButton";
		closeButton.Size = new System.Drawing.Size(458, 27);
		closeButton.TabIndex = 7;
		closeButton.Text = "CloseAction";
		closeButton.UseVisualStyleBackColor = true;
		// 
		// UpdateCheckForm
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		AutoSize = true;
		AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		CancelButton = closeButton;
		ClientSize = new System.Drawing.Size(464, 321);
		Controls.Add(tableLayoutPanel);
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimumSize = new System.Drawing.Size(480, 360);
		Name = "UpdateCheckForm";
		ShowIcon = false;
		Text = "UpdateCheckTitle";
		Load += CheckForUpdates;
		tableLayoutPanel.ResumeLayout(false);
		tableLayoutPanel.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
	private System.Windows.Forms.Label updateCheckStatusLabel;
	private System.Windows.Forms.Label updateCheckDetailsLabel;
	private System.Windows.Forms.RichTextBox updateCheckDetailsRichTextBox;
	private System.Windows.Forms.Label latestVersionLabel;
	private System.Windows.Forms.Label currentVersionLabel;
	private System.Windows.Forms.Button viewUpdateButton;
	private System.Windows.Forms.Button closeButton;
}
