// https://pws.yarb00.dev

namespace PWSandbox;

partial class PlayForm
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
		SuspendLayout();
		// 
		// PlayForm
		// 
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		ClientSize = new System.Drawing.Size(0, 0);
		DoubleBuffered = true;
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		KeyPreview = true;
		MaximizeBox = false;
		Name = "PlayForm";
		ShowIcon = false;
		Text = "PlayModeTitle";
		DpiChanged += UpdateScaling;
		Paint += ProcessMap;
		KeyDown += OnKeyDown;
		ResumeLayout(false);
	}

	#endregion
}
