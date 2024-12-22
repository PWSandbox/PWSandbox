// This file is a part of PWSandbox ( https://github.com/yarb00/PWSandbox )

namespace PWSandbox;

partial class MenuForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        appNameLabel = new Label();
        githubLinkLabel = new Label();
        appVersionLabel = new Label();
        authorLabel = new Label();
        mapFileLocationTextBox = new TextBox();
        loadMapFileButton = new Button();
        SuspendLayout();
        // 
        // appNameLabel
        // 
        appNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        appNameLabel.AutoSize = true;
        appNameLabel.Font = new Font("Consolas", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
        appNameLabel.Location = new Point(194, 9);
        appNameLabel.Name = "appNameLabel";
        appNameLabel.Size = new Size(258, 56);
        appNameLabel.TabIndex = 0;
        appNameLabel.Text = "PWSandbox";
        appNameLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // githubLinkLabel
        // 
        githubLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        githubLinkLabel.AutoSize = true;
        githubLinkLabel.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
        githubLinkLabel.Location = new Point(256, 65);
        githubLinkLabel.Name = "githubLinkLabel";
        githubLinkLabel.Size = new Size(196, 14);
        githubLinkLabel.TabIndex = 1;
        githubLinkLabel.Text = "github.com/yarb00/pwsandbox";
        githubLinkLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // appVersionLabel
        // 
        appVersionLabel.AutoSize = true;
        appVersionLabel.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
        appVersionLabel.Location = new Point(12, 9);
        appVersionLabel.Name = "appVersionLabel";
        appVersionLabel.Size = new Size(49, 14);
        appVersionLabel.TabIndex = 2;
        appVersionLabel.Text = "v1.1.0";
        // 
        // authorLabel
        // 
        authorLabel.AutoSize = true;
        authorLabel.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
        authorLabel.Location = new Point(12, 23);
        authorLabel.Name = "authorLabel";
        authorLabel.Size = new Size(70, 14);
        authorLabel.TabIndex = 3;
        authorLabel.Text = "by yarb00";
        // 
        // mapFileLocationTextBox
        // 
        mapFileLocationTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        mapFileLocationTextBox.Location = new Point(12, 287);
        mapFileLocationTextBox.Name = "mapFileLocationTextBox";
        mapFileLocationTextBox.PlaceholderText = "Path to map (*.pwsandbox) file";
        mapFileLocationTextBox.Size = new Size(354, 22);
        mapFileLocationTextBox.TabIndex = 4;
        mapFileLocationTextBox.Text = "map.pwsandbox";
        // 
        // loadMapFileButton
        // 
        loadMapFileButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        loadMapFileButton.Location = new Point(372, 288);
        loadMapFileButton.Name = "loadMapFileButton";
        loadMapFileButton.Size = new Size(80, 21);
        loadMapFileButton.TabIndex = 5;
        loadMapFileButton.Text = "Load map!";
        loadMapFileButton.UseVisualStyleBackColor = true;
		loadMapFileButton.Click += LoadMap;
        // 
        // MenuForm
        // 
        AutoScaleDimensions = new SizeF(7F, 14F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(464, 321); // 480x360
        Controls.Add(loadMapFileButton);
        Controls.Add(mapFileLocationTextBox);
        Controls.Add(authorLabel);
        Controls.Add(appVersionLabel);
        Controls.Add(githubLinkLabel);
        Controls.Add(appNameLabel);
        Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MaximumSize = new Size(480, 360);
        MinimumSize = new Size(480, 360);
        Name = "MenuForm";
        ShowIcon = false;
        Text = "PWSandbox";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label appNameLabel;
    private Label githubLinkLabel;
    private Label appVersionLabel;
    private Label authorLabel;
    private TextBox mapFileLocationTextBox;
    private Button loadMapFileButton;
}
