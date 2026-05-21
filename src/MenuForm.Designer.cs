// https://pws.yarb00.dev

namespace PWSandbox;

partial class MenuForm
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
		loadMapButton = new System.Windows.Forms.Button();
		aboutAppButton = new System.Windows.Forms.Button();
		checkForUpdatesButton = new System.Windows.Forms.Button();
		languageLabel = new System.Windows.Forms.Label();
		languageComboBox = new System.Windows.Forms.ComboBox();
		openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
		tableLayoutPanel.Controls.Add(loadMapButton, 0, 1);
		tableLayoutPanel.Controls.Add(aboutAppButton, 0, 2);
		tableLayoutPanel.Controls.Add(checkForUpdatesButton, 0, 3);
		tableLayoutPanel.Controls.Add(languageLabel, 0, 4);
		tableLayoutPanel.Controls.Add(languageComboBox, 0, 5);
		tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
		tableLayoutPanel.Name = "tableLayoutPanel";
		tableLayoutPanel.RowCount = 6;
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
		tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
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
		// loadMapButton
		// 
		loadMapButton.AutoSize = true;
		loadMapButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		loadMapButton.Dock = System.Windows.Forms.DockStyle.Fill;
		loadMapButton.Location = new System.Drawing.Point(3, 67);
		loadMapButton.Name = "loadMapButton";
		loadMapButton.Size = new System.Drawing.Size(258, 58);
		loadMapButton.TabIndex = 2;
		loadMapButton.Text = "LoadMapAction";
		loadMapButton.UseVisualStyleBackColor = true;
		loadMapButton.Click += LoadMap;
		// 
		// aboutAppButton
		// 
		aboutAppButton.AutoSize = true;
		aboutAppButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		aboutAppButton.Dock = System.Windows.Forms.DockStyle.Fill;
		aboutAppButton.Location = new System.Drawing.Point(3, 131);
		aboutAppButton.Name = "aboutAppButton";
		aboutAppButton.Size = new System.Drawing.Size(258, 58);
		aboutAppButton.TabIndex = 3;
		aboutAppButton.Text = "AboutAppAction";
		aboutAppButton.UseVisualStyleBackColor = true;
		aboutAppButton.Click += OpenAboutAppDialog;
		// 
		// checkForUpdatesButton
		// 
		checkForUpdatesButton.AutoSize = true;
		checkForUpdatesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		checkForUpdatesButton.Dock = System.Windows.Forms.DockStyle.Fill;
		checkForUpdatesButton.Location = new System.Drawing.Point(3, 195);
		checkForUpdatesButton.Name = "checkForUpdatesButton";
		checkForUpdatesButton.Size = new System.Drawing.Size(258, 58);
		checkForUpdatesButton.TabIndex = 4;
		checkForUpdatesButton.Text = "CheckForUpdatesAction";
		checkForUpdatesButton.UseVisualStyleBackColor = true;
		checkForUpdatesButton.Click += OpenUpdaterDialog;
		// 
		// languageLabel
		// 
		languageLabel.AutoSize = true;
		languageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		languageLabel.Location = new System.Drawing.Point(3, 256);
		languageLabel.Name = "languageLabel";
		languageLabel.Size = new System.Drawing.Size(258, 32);
		languageLabel.TabIndex = 5;
		languageLabel.Text = "LanguageSection";
		languageLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		// 
		// languageComboBox
		// 
		languageComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
		languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		languageComboBox.FormattingEnabled = true;
		languageComboBox.Location = new System.Drawing.Point(3, 291);
		languageComboBox.Name = "languageComboBox";
		languageComboBox.Size = new System.Drawing.Size(258, 23);
		languageComboBox.TabIndex = 6;
		languageComboBox.SelectedIndexChanged += ChangeLanguage;
		// 
		// openFileDialog
		// 
		openFileDialog.DefaultExt = "pws_map";
		openFileDialog.Filter = "PWSandbox maps|*.pws_map";
		openFileDialog.Title = "MapFileSelectionTitle";
		// 
		// MenuForm
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		AutoSize = true;
		AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		ClientSize = new System.Drawing.Size(264, 321);
		Controls.Add(tableLayoutPanel);
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimumSize = new System.Drawing.Size(280, 360);
		Name = "MenuForm";
		ShowIcon = false;
		Text = "PWSandbox";
		tableLayoutPanel.ResumeLayout(false);
		tableLayoutPanel.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
	private System.Windows.Forms.Label appNameLabel;
	private System.Windows.Forms.Button loadMapButton;
	private System.Windows.Forms.Button aboutAppButton;
	private System.Windows.Forms.Button checkForUpdatesButton;
	private System.Windows.Forms.Label languageLabel;
	private System.Windows.Forms.ComboBox languageComboBox;
	private System.Windows.Forms.OpenFileDialog openFileDialog;
}
