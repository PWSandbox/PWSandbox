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
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(464, 321); // 480x360
        DoubleBuffered = true;
        KeyPreview = true;
        Name = "PlayForm";
        ShowIcon = false;
        Text = "PWSandbox - PLAY mode";
        KeyDown += OnKeyDown;
        ResumeLayout(false);
    }

    #endregion
}
