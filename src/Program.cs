// https://pws.yarb00.dev

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWSandbox;

internal static class Program
{
	public const string Website = "https://pws.yarb00.dev";

	public static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version ?? throw new UnreachableException("Assembly version can't be null.");
	public static readonly string FriendlyVersion = Version.ToString(3);

	[STAThread]
	public static void Main(string[] args)
	{
		if (!Debugger.IsAttached)
		{
			AppDomain.CurrentDomain.UnhandledException += static (_, e) => HandleUnhandledException(e.ExceptionObject as Exception);
			TaskScheduler.UnobservedTaskException += static (_, e) => HandleUnhandledException(e.Exception);

			Application.ThreadException += static (_, e) => HandleUnhandledException(e.Exception, true);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		}

		ApplicationConfiguration.Initialize();

		if (args.Length == 0) Application.Run(new MenuForm());
		else Application.Run(new ArgumentsApplicationContext(args));
	}

	private static void HandleUnhandledException(Exception? e, bool isResumingAllowed = false)
	{
		const string
			baseText = $"""
				An unhandled exception occurred!
				It's a serious error that should not normally happen.

				<a href="{Website}/issue/report/client">Report this bug here</a>.
				""",
			resumingNotAllowedText = $"""
				{baseText}

				You can press the "Close" button below to exit PWSandbox.
				""",
			resumingAllowedText = $"""
				{baseText}

				You can either
				- press the "Close" button below to exit PWSandbox
				- or press the "Continue" button below to try ignore this error (stability is not guaranteed!).
				""";

		TaskDialogButtonCollection buttons = [TaskDialogButton.Close];
		if (isResumingAllowed) buttons.Add(TaskDialogButton.Continue);

		TaskDialogPage unhandledExceptionDialog = new()
		{
			Caption = "PWSandbox: Unhandled exception",
			Heading = "ouch x_x",
			Icon = TaskDialogIcon.ShieldErrorRedBar,
			SizeToContent = true,
			EnableLinks = true,

			Buttons = buttons,

			Expander = new TaskDialogExpander
			{
				CollapsedButtonText = "Show details",
				ExpandedButtonText = "Hide details",
				Text = $"""
					===== Details: =====
					{e?.ToString() ?? "[Details are not available.]"}
					"""
			},

			Text = isResumingAllowed ? resumingAllowedText : resumingNotAllowedText
		};

		unhandledExceptionDialog.LinkClicked += (_, e) => Process.Start(new ProcessStartInfo(e.LinkHref) { UseShellExecute = true });

		TaskDialogButton result = TaskDialog.ShowDialog(unhandledExceptionDialog);
		if (result == TaskDialogButton.Close) Environment.FailFast("An unhandled exception occurred.", e);
	}

	private class ArgumentsApplicationContext : ApplicationContext
	{
		private int openForms = 0;

		public ArgumentsApplicationContext(string[] filePaths)
		{
			int totalMaps = filePaths.Length, successfulMaps = 0, failedMaps = 0;
			List<string> results = [];

			foreach (string filePath in filePaths)
			{
				(PlayForm? playForm, string? errorText) = MenuForm.GetLoadedPlayForm(filePath);

				if (errorText is not null)
				{
					failedMaps++;
					results.Add($"""
						Map "{filePath}"
						could not load because of an error:
						"{errorText}"
						""");
				}

				if (playForm is not null)
				{
					playForm.FormClosed += (_, _) => OnFormClosed();
					playForm.Show();
					openForms++;

					successfulMaps++;
					results.Add($"""
						Map "{filePath}"
						loaded successfully.
						""");
				}
			}

			TaskDialog.ShowDialog(new()
			{
				Caption = "PWSandbox: Results",
				Heading = "Results",
				Icon = TaskDialogIcon.Information,
				SizeToContent = true,

				Buttons = [TaskDialogButton.OK],

				Expander = new TaskDialogExpander
				{
					CollapsedButtonText = "Show details",
					ExpandedButtonText = "Hide details",
					Text = $"""
						===== Details: =====
						{string.Join(Environment.NewLine + Environment.NewLine, results)}
						"""
				},

				Text = $"""
					Tried to load {totalMaps} maps.
					{successfulMaps} of them loaded successfully,
					{failedMaps} with errors.
					"""
			});
		}

		private void OnFormClosed()
		{
			openForms--;
			if (openForms == 0) ExitThread();
		}
	}
}
