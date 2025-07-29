using Avalonia;
using System;
using System.Reflection;

namespace PWSandbox;

static class Program
{
	public static readonly Version? Version = Assembly.GetExecutingAssembly().GetName().Version;

	[STAThread]
	public static void Main(string[] args) => BuildAvaloniaApp()
		.StartWithClassicDesktopLifetime(args);

	private static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
		.UsePlatformDetect()
		.WithInterFont()
		.LogToTrace();
}
