// This file is a part of PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// PWSandbox is licensed under the MIT (Expat) License:

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PWSandbox;

static class Program
{
	[STAThread]
	static void Main(string[] args)
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
						loaded succesfully.
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
					{successForms} of them loaded succesfully,
					{errorForms} with errors.
					"""
			};
			resultsDialog.Destroyed += OnFormClosed;
			TaskDialog.ShowDialog(resultsDialog);
			openForms++;
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
			rawIssueCreateLink = "https://github.com/PWSandbox/PWSandbox/issues/new",
			resumingNotAllowedText = $"""
				An unhandled exception occured! An unhandled exception is a serious error that should not normally happen.

				Please report this issue to the <a href="{rawIssueCreateLink}">PWSandbox GitHub</a> (<-- this link is clickable!).

				After you do so, press the "Close" button below to exit PWSandbox.
				""",
			resumingAllowedText = $"""
				An unhandled exception occured! An unhandled exception is a serious error that should not normally happen.

				Please report this issue to the <a href="{rawIssueCreateLink}">PWSandbox GitHub</a> (<-- this link is clickable!).

				After you do so, you can either
				press the "Close" button below to exit PWSandbox
				or press the "Continue" button below to ignore the error and try to return to PWSandbox (stability is not guaranteed!).
				""";

		string
			issueTitle = $"Unhandled exception: \"{e.GetType().Name}\"",
			issueBody = $"""
				***Auto-generated by PWSandbox.***

				## Environment
				**PWSandbox version**: `{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "[! REPLACE WITH YOUR PWSANDBOX VERSION !]"}`
				**OS**: `{System.Runtime.InteropServices.RuntimeInformation.OSDescription}`

				## Exception info
				**Type**: `{e.GetType().FullName}`
				**Message**: `{e.Message}`

				### Stack trace
				{e.StackTrace?.Replace("   ", "    ") ?? "[! REPLACE WITH SCREENSHOT OF YOUR ERROR WINDOW WITH DETAILS SHOWED !]"}
				""",
			issueCreateLink = $"{rawIssueCreateLink}?title={Linkify(issueTitle)}&body={Linkify(issueBody)}";

		TaskDialogButtonCollection unhandledExceptionDialogButtons = [TaskDialogButton.Close];
		if (isResumingAllowed) unhandledExceptionDialogButtons.Add(TaskDialogButton.Continue);

		TaskDialogPage unhandledExceptionDialog = new()
		{
			Caption = "PWSandbox: An unhandled exception occured!",
			Heading = "ouch x_x",
			Icon = TaskDialogIcon.ShieldErrorRedBar,
			SizeToContent = true,
			EnableLinks = true,

			Buttons = unhandledExceptionDialogButtons,

			Expander = new TaskDialogExpander
			{
				CollapsedButtonText = "Show details",
				ExpandedButtonText = "Hide details",
				Text = "===== Details: =====" + '\n' + '\n' + e
			},

			Text = (isResumingAllowed ? resumingAllowedText : resumingNotAllowedText).Replace(rawIssueCreateLink, issueCreateLink)
		};

		unhandledExceptionDialog.LinkClicked += (sender, e) => Process.Start(
			new ProcessStartInfo(e.LinkHref)
			{ UseShellExecute = true }
		);

		TaskDialogButton result = TaskDialog.ShowDialog(unhandledExceptionDialog);
		if (result == TaskDialogButton.Close) Application.Exit();
	}

	private static string Linkify(string text) => text
		.Replace(' ', '+')
		.Replace("#", "%23")
		.Replace("\"", "%22")
		.Replace("/", "%2F")
		.Replace("&", "%26")
		.ReplaceLineEndings("%0A");

	#endregion
}
