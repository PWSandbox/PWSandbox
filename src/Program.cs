// PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// Licensed under the MIT (Expat) license; Copyright (c) 2024-2025 yarb00

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace PWSandbox;

static class Program
{
	public static readonly Version? Version = Assembly.GetExecutingAssembly().GetName().Version;

	[STAThread]
	public static void Main(string[] args)
	{
		if (!Debugger.IsAttached)
		{
			AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

			Application.ThreadException += HandleUnhandledException;
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		}

		Application.EnableVisualStyles();
		Application.SetDefaultFont(new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0));

		if (args.Length == 0) Application.Run(new MenuForm());
		else Application.Run(new CommandLineArgumentsApplicationContext(args));
	}

	class CommandLineArgumentsApplicationContext : ApplicationContext
	{
		private int openForms = 0;

		internal CommandLineArgumentsApplicationContext(string[] filePaths)
		{
			int totalForms = filePaths.Length, successForms = 0, errorForms = 0;
			List<string> results = [];

			foreach (string filePath in filePaths)
			{
				(PlayForm? playForm, string? errorText) = MenuForm.GetLoadedPlayForm(filePath);

				if (errorText is not null)
				{
					errorForms++;
					results.Add($"""
						Map "{filePath}"
						could not load because of an error:
						"{errorText}"
						""");
				}

				if (playForm is not null)
				{
					playForm.Closed += OnFormClosed;
					playForm.Show();
					openForms++;

					successForms++;
					results.Add($"""
						Map "{filePath}"
						loaded successfully.
						""");
				}
			}

			TaskDialogPage resultsDialog = new()
			{
				Caption = "PWSandbox: Loading results",
				Heading = "Loading results",
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
					Tried to load {totalForms} maps.
					{successForms} of them loaded successfully,
					{errorForms} with errors.
					"""
			};
			TaskDialog.ShowDialog(resultsDialog);
		}

		private void OnFormClosed(object? sender, EventArgs e)
		{
			if (--openForms == 0) ExitThread();
		}
	}

	#region Handling of unhandled exceptions

	private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
		=> HandleUnhandledException((Exception)e.ExceptionObject);

	private static void HandleUnhandledException(object sender, ThreadExceptionEventArgs e)
		=> HandleUnhandledException(e.Exception, true);

	private static void HandleUnhandledException(Exception e, bool isResumingAllowed = false)
	{
		const string
			baseText = $"""
				An unhandled exception occurred!
				It's a serious error that should not normally happen.

				NOTE: Please do not report this issue to the PWSandbox GitHub.
				The version you are using is legacy and is not supported anymore.
				To receive updates, new features and bug fixes, migrate to the latest version of PWSandbox.
				For more details, visit <a href="https://github.com/PWSandbox/PWSandbox">PWSandbox GitHub</a>.
				""",
			resumingNotAllowedText = $"""
				{baseText}

				Now press the "Close" button below to exit PWSandbox.
				""",
			resumingAllowedText = $"""
				{baseText}

				Now you can either
				- press the "Close" button below to exit PWSandbox
				- or press the "Continue" button below to ignore the error
				and try to return to PWSandbox (stability is not guaranteed!).
				""";

		string
			exceptionMessage = e.Message,
			innerExceptionMessage = e.InnerException?.Message ?? "[Not present.]",
			exceptionDetails = e.StackTrace ?? "[Not present.]",
			innerExceptionDetails = e.InnerException?.StackTrace ?? "[Not present.]";

		TaskDialogButtonCollection unhandledExceptionDialogButtons = [TaskDialogButton.Close];
		if (isResumingAllowed) unhandledExceptionDialogButtons.Add(TaskDialogButton.Continue);

		TaskDialogPage unhandledExceptionDialog = new()
		{
			Caption = "Critical error! | PWSandbox",
			Heading = "ouch x_x",
			Icon = TaskDialogIcon.ShieldErrorRedBar,
			SizeToContent = true,
			EnableLinks = true,

			Buttons = unhandledExceptionDialogButtons,

			Expander = new TaskDialogExpander
			{
				CollapsedButtonText = "Show details",
				ExpandedButtonText = "Hide details",
				Text = $"""
					========== Details: ==========

					Message (inner exception): {innerExceptionMessage}
					Message: {exceptionMessage}

					----- Stack trace (inner exception): -----
					{innerExceptionDetails}

					----- Stack trace: -----
					{exceptionDetails}
					"""
			},

			Text = isResumingAllowed ? resumingAllowedText : resumingNotAllowedText
		};

		unhandledExceptionDialog.LinkClicked += (s, e) => Process.Start(
			new ProcessStartInfo(e.LinkHref) { UseShellExecute = true }
		);

		TaskDialogButton result = TaskDialog.ShowDialog(unhandledExceptionDialog);
		if (result == TaskDialogButton.Close) Application.Exit();
	}

	#endregion
}
