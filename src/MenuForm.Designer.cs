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
		appNameLabel = new Label();
		mapFileLocationTextBox = new TextBox();
		loadMapFileButton = new Button();
		aboutAppButton = new Button();
		appVersionLabel = new Label();
		SuspendLayout();
		//
		// appNameLabel
		//
		appNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		appNameLabel.Font = new Font("Consolas", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
		appNameLabel.Location = new Point(12, 9);
		appNameLabel.Name = "appNameLabel";
		appNameLabel.Size = new Size(440, 56);
		appNameLabel.TabIndex = 0;
		appNameLabel.Text = "PWSandbox";
		appNameLabel.TextAlign = ContentAlignment.MiddleCenter;
		appNameLabel.Click += SwitchTheme;
		//
		// mapFileLocationTextBox
		//
		mapFileLocationTextBox.Location = new Point(12, 68);
		mapFileLocationTextBox.Name = "mapFileLocationTextBox";
		mapFileLocationTextBox.Size = new Size(354, 22);
		mapFileLocationTextBox.TabIndex = 4;
		mapFileLocationTextBox.Text = "map.pws_map";
		//
		// loadMapFileButton
		//
		loadMapFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		loadMapFileButton.FlatStyle = FlatStyle.Flat;
		loadMapFileButton.Font = new Font("Consolas", 7F);
		loadMapFileButton.Location = new Point(372, 69);
		loadMapFileButton.Name = "loadMapFileButton";
		loadMapFileButton.Size = new Size(80, 21);
		loadMapFileButton.TabIndex = 5;
		loadMapFileButton.Text = "Load map!";
		loadMapFileButton.UseVisualStyleBackColor = false;
		loadMapFileButton.Click += LoadMapFile;
		//
		// aboutAppButton
		//
		aboutAppButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		aboutAppButton.FlatStyle = FlatStyle.Flat;
		aboutAppButton.Location = new Point(12, 106);
		aboutAppButton.Name = "aboutAppButton";
		aboutAppButton.Size = new Size(440, 23);
		aboutAppButton.TabIndex = 7;
		aboutAppButton.Text = "About PWSandbox";
		aboutAppButton.UseVisualStyleBackColor = false;
		aboutAppButton.Click += OpenAboutAppDialog;
		//
		// appVersionLabel
		//
		appVersionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		appVersionLabel.Location = new Point(372, 9);
		appVersionLabel.Name = "appVersionLabel";
		appVersionLabel.Size = new Size(80, 14);
		appVersionLabel.TabIndex = 10;
		appVersionLabel.Text = "Version ?";
		appVersionLabel.TextAlign = ContentAlignment.TopRight;
		//
		// MenuForm
		//
		AutoScaleMode = AutoScaleMode.None;
		BackColor = Color.Black;
		ClientSize = new Size(464, 141);
		Controls.Add(appVersionLabel);
		Controls.Add(aboutAppButton);
		Controls.Add(loadMapFileButton);
		Controls.Add(mapFileLocationTextBox);
		Controls.Add(appNameLabel);
		Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
		ForeColor = Color.White;
		FormBorderStyle = FormBorderStyle.FixedSingle;
		MaximizeBox = false;
		MaximumSize = new Size(480, 180);
		MinimumSize = new Size(480, 180);
		Name = "MenuForm";
		ShowIcon = false;
		Text = "PWSandbox";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label appNameLabel;
	private TextBox mapFileLocationTextBox;
	private Button loadMapFileButton;
	private Button aboutAppButton;
	private Label appVersionLabel;
}
