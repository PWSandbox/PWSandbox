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

partial class MenuForm
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
		appNameLabel = new System.Windows.Forms.Label();
		aboutAppButton = new System.Windows.Forms.Button();
		switchColorThemeButton = new System.Windows.Forms.Button();
		checkForUpdatesButton = new System.Windows.Forms.Button();
		mapOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
		loadMapFileButton = new System.Windows.Forms.Button();
		SuspendLayout();
		//
		// appNameLabel
		//
		appNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appNameLabel.Font = new System.Drawing.Font("Consolas", 36F);
		appNameLabel.Location = new System.Drawing.Point(12, 9);
		appNameLabel.Name = "appNameLabel";
		appNameLabel.Size = new System.Drawing.Size(288, 56);
		appNameLabel.TabIndex = 0;
		appNameLabel.Text = "PWSandbox";
		appNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		//
		// aboutAppButton
		//
		aboutAppButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		aboutAppButton.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		aboutAppButton.FlatAppearance.BorderSize = 0;
		aboutAppButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
		aboutAppButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
		aboutAppButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		aboutAppButton.Location = new System.Drawing.Point(159, 126);
		aboutAppButton.Name = "aboutAppButton";
		aboutAppButton.Size = new System.Drawing.Size(141, 23);
		aboutAppButton.TabIndex = 4;
		aboutAppButton.Text = "&About PWSandbox";
		aboutAppButton.UseVisualStyleBackColor = false;
		aboutAppButton.Click += OpenAboutAppDialog;
		//
		// switchColorThemeButton
		//
		switchColorThemeButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		switchColorThemeButton.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		switchColorThemeButton.FlatAppearance.BorderSize = 0;
		switchColorThemeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
		switchColorThemeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
		switchColorThemeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		switchColorThemeButton.Location = new System.Drawing.Point(159, 97);
		switchColorThemeButton.Name = "switchColorThemeButton";
		switchColorThemeButton.Size = new System.Drawing.Size(141, 23);
		switchColorThemeButton.TabIndex = 3;
		switchColorThemeButton.Text = "&Switch color theme";
		switchColorThemeButton.UseVisualStyleBackColor = false;
		switchColorThemeButton.Click += SwitchTheme;
		//
		// checkForUpdatesButton
		//
		checkForUpdatesButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		checkForUpdatesButton.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		checkForUpdatesButton.FlatAppearance.BorderSize = 0;
		checkForUpdatesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
		checkForUpdatesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
		checkForUpdatesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		checkForUpdatesButton.Location = new System.Drawing.Point(159, 68);
		checkForUpdatesButton.Name = "checkForUpdatesButton";
		checkForUpdatesButton.Size = new System.Drawing.Size(141, 23);
		checkForUpdatesButton.TabIndex = 2;
		checkForUpdatesButton.Text = "&Check for updates";
		checkForUpdatesButton.UseVisualStyleBackColor = false;
		checkForUpdatesButton.Click += CheckForUpdates;
		//
		// mapOpenFileDialog
		//
		mapOpenFileDialog.DefaultExt = "pws_map";
		mapOpenFileDialog.Filter = "PWSandbox map files|*.pws_map";
		mapOpenFileDialog.Title = "Select a PWSandbox map...";
		//
		// loadMapFileButton
		//
		loadMapFileButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		loadMapFileButton.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		loadMapFileButton.FlatAppearance.BorderSize = 0;
		loadMapFileButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
		loadMapFileButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
		loadMapFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		loadMapFileButton.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		loadMapFileButton.Location = new System.Drawing.Point(12, 68);
		loadMapFileButton.Name = "loadMapFileButton";
		loadMapFileButton.Size = new System.Drawing.Size(141, 81);
		loadMapFileButton.TabIndex = 1;
		loadMapFileButton.Text = "&Load map!";
		loadMapFileButton.UseVisualStyleBackColor = false;
		loadMapFileButton.Click += LoadMapFile;
		//
		// MenuForm
		//
		AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		BackColor = System.Drawing.Color.Black;
		ClientSize = new System.Drawing.Size(312, 159);
		Controls.Add(loadMapFileButton);
		Controls.Add(checkForUpdatesButton);
		Controls.Add(switchColorThemeButton);
		Controls.Add(aboutAppButton);
		Controls.Add(appNameLabel);
		Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		ForeColor = System.Drawing.Color.White;
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		MaximizeBox = false;
		MaximumSize = new System.Drawing.Size(328, 198);
		MinimumSize = new System.Drawing.Size(328, 198);
		Name = "MenuForm";
		ShowIcon = false;
		Text = "PWSandbox";
		ResumeLayout(false);
	}

	#endregion

	private System.Windows.Forms.Label appNameLabel;
	private System.Windows.Forms.Button aboutAppButton;
	private System.Windows.Forms.Button switchColorThemeButton;
	private System.Windows.Forms.Button checkForUpdatesButton;
	private System.Windows.Forms.OpenFileDialog mapOpenFileDialog;
	private System.Windows.Forms.Button loadMapFileButton;
}
