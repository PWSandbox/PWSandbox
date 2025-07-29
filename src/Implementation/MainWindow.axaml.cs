using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PWSandbox;

sealed partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		if (Program.Version is not null) Title += $" v{Program.Version.ToString(3)}";
	}

	private void MakeSure(object? sender, RoutedEventArgs e) => MainText.Text = "Yeah!";
}
