// This file is a part of PWSandbox ( https://github.com/yarb00/PWSandbox )

namespace PWSandbox;

partial class PlayForm
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
        SuspendLayout();
        // 
        // PlayForm
        // 
        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(464, 321);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        KeyPreview = true;
        Name = "PlayForm";
        ShowIcon = false;
        Text = "PWSandbox - PLAY mode";
        KeyDown += OnKeyDown;
        ResumeLayout(false);
    }

    #endregion
}
