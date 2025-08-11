// PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// Licensed under the MIT (Expat) license; Copyright (c) 2024-2025 yarb00

namespace PWSandbox;

partial class PlayForm
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
		SuspendLayout();
		//
		// PlayForm
		//
		AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		BackColor = System.Drawing.Color.Black;
		ClientSize = new System.Drawing.Size(464, 321);
		DoubleBuffered = true;
		ForeColor = System.Drawing.Color.White;
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		KeyPreview = true;
		MaximizeBox = false;
		Name = "PlayForm";
		ShowIcon = false;
		Text = "PWSandbox [Play]";
		KeyDown += OnKeyDown;
		ResumeLayout(false);
	}

	#endregion
}
