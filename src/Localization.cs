// https://pws.yarb00.dev

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PWSandbox;

internal enum StringId
{
	UnhandledExceptionTitle, UnhandledExceptionResumingNotAllowedText, UnhandledExceptionResumingAllowedText, ShowDetailsAction, HideDetailsAction, DetailsSection,
	MapFileSelectionTitle, MapFileErrorTitle, MapFileErrorText, LoadMapAction, AboutAppAction, CheckForUpdatesAction, LanguageSection,
	PlayModeTitle, FinishReachedText, MapLoadingStatus, MapLoadedSuccessfullyText, MapLoadingFailedText,
	DescriptionSection, DescriptionText, LicenseSection, VersionText, OkAction,
	UpdateCheckTitle, LatestVersionInfoText, CurrentVersionInfoText, SeeUpdateAction, CloseAction,
	CheckingForUpdatesStatus, ErrorStatus, UpdateAvailableStatus, NoUpdateAvailableStatus,
	CheckingForUpdatesStatusDetails, ErrorStatusDetails, UpdateAvailableStatusDetails, NoUpdateAvailableStatusDetails
}

internal readonly record struct Language(string LanguageId, string InternationalName, string SelfName, bool IsRightToLeft)
{
	public override string ToString() => $"[{LanguageId}] {InternationalName} ({SelfName})";
}

internal static class Localization
{
	public static IReadOnlyList<Language> AvailableLanguages => availableLanguages; // All loaded languages

	public static IReadOnlyDictionary<StringId, string> StringById { get; private set; } // Mapping of StringId to the translation text for the current language

	public static Language CurrentLanguage { get; private set; }

	public static bool AreStringsRightToLeft => CurrentLanguage.IsRightToLeft;

	public static event EventHandler? LocalizationChanged;

	private const string defaultLanguageId = "en";

#if DEBUG
	private const string localizationTestText = "أنا اجيب أن أبي. أبي هو يعمل كثيرا و يساعدوني في حياتي. أبي هو لطيف و محبوب. هو يحب أن يساعد الناس هو لا يسكن في بيت.";
#endif

	private static readonly List<Language> availableLanguages = [
		new Language
		{
			LanguageId = "en",
			InternationalName = "English",
			SelfName = "English",
			IsRightToLeft = false
		},
#if DEBUG
		new Language
		{
			LanguageId = "test",
			InternationalName = "Localization test",
			SelfName = "🧪",
			IsRightToLeft = true
		}
#endif
	];

	private static readonly Dictionary<string, Dictionary<StringId, string>> localizationByLanguageId = new(StringComparer.OrdinalIgnoreCase) // Mappings of StringId to the translation text for every available language
	{
		["en"] = new()
		{
			[StringId.UnhandledExceptionTitle] = "PWSandbox: Unhandled exception!",
			[StringId.UnhandledExceptionResumingNotAllowedText] = """
				An unhandled exception occurred!
				It's a serious error that should not normally happen.

				\(REPORT_LINK_START)Report this bug here\(REPORT_LINK_END).

				You can press the "Close" button below to exit PWSandbox.
				""",
			[StringId.UnhandledExceptionResumingAllowedText] = """
				An unhandled exception occurred!
				It's a serious error that should not normally happen.

				\(REPORT_LINK_START)Report this bug here\(REPORT_LINK_END).

				You can either
				- press the "Close" button below to exit PWSandbox
				- or press the "Continue" button below to try ignoring this error (stability is not guaranteed!).
				""",
			[StringId.ShowDetailsAction] = "Show details",
			[StringId.HideDetailsAction] = "Hide details",
			[StringId.DetailsSection] = "===== Details: =====",
			[StringId.MapFileSelectionTitle] = "Select a PWSandbox map...",
			[StringId.MapFileErrorTitle] = "PWSandbox [Play]: Error!",
			[StringId.MapFileErrorText] = """
				Map file is not valid!
				It's either made for a newer version of PWSandbox or just written incorrectly.

				Contact the map maker and let them know about this problem.
				(If you are the map maker and map file is being loaded with the right version of PWSandbox,
				then you wrote map file in a wrong way. Check detailed message.)
				""",
			[StringId.LoadMapAction] = "Load map!",
			[StringId.AboutAppAction] = "About PWSandbox",
			[StringId.CheckForUpdatesAction] = "Check for updates",
			[StringId.LanguageSection] = "Language:",
			[StringId.PlayModeTitle] = "PWSandbox [Play]",
			[StringId.FinishReachedText] = "You have reached the finish!",
			[StringId.MapLoadingStatus] = """
				Tried to load \(TOTAL_MAPS) maps.
				\(SUCCESSFUL_MAPS) of them loaded successfully,
				and PWSandbox wasn't able to load \(FAILED_MAPS) of them.
				""",
			[StringId.MapLoadedSuccessfullyText] = """
				Map "\(MAP)"
				loaded successfully.
				""",
			[StringId.MapLoadingFailedText] = """
				An error occurred when loading map "\(MAP)":
				\(ERROR_MESSAGE)
				""",
			[StringId.DescriptionSection] = "Description:",
			[StringId.DescriptionText] = """
				Simple sandbox game, built with .NET and Windows Forms.

				Website: \(WEBSITE_LINK)
				""",
			[StringId.LicenseSection] = "License:",
			[StringId.VersionText] = "Version \\(VERSION), \\(BUILD_TYPE) build, \\(COMPILATION_TYPE)",
			[StringId.OkAction] = "OK",
			[StringId.UpdateCheckTitle] = "PWSandbox Updater",
			[StringId.LatestVersionInfoText] = "Latest version: \\(VERSION)",
			[StringId.CurrentVersionInfoText] = "Installed version: \\(VERSION)",
			[StringId.SeeUpdateAction] = "View update",
			[StringId.CloseAction] = "Close",
			[StringId.CheckingForUpdatesStatus] = "Checking for updates...",
			[StringId.ErrorStatus] = "Error!",
			[StringId.UpdateAvailableStatus] = "An update is available!",
			[StringId.NoUpdateAvailableStatus] = "No updates!",
			[StringId.CheckingForUpdatesStatusDetails] = "Fetching the update data from the server...",
			[StringId.ErrorStatusDetails] = """
				An error occurred while checking for updates:

				\(ERROR_MESSAGE)
				""",
			[StringId.UpdateAvailableStatusDetails] = "To see more info about the new PWSandbox update, press the \"View update\" button.",
			[StringId.NoUpdateAvailableStatusDetails] = "Congratulations! You're using the latest version."
		},
#if DEBUG
		["test"] = new()
		{
			[StringId.UnhandledExceptionTitle] = localizationTestText,
			[StringId.UnhandledExceptionResumingNotAllowedText] = "\\(REPORT_LINK_START)!!!\\(REPORT_LINK_END)",
			[StringId.UnhandledExceptionResumingAllowedText] = "\\(REPORT_LINK_START)!!!\\(REPORT_LINK_END)",
			[StringId.ShowDetailsAction] = localizationTestText,
			[StringId.HideDetailsAction] = localizationTestText,
			[StringId.DetailsSection] = localizationTestText,
			[StringId.MapFileSelectionTitle] = localizationTestText,
			[StringId.MapFileErrorTitle] = localizationTestText,
			[StringId.MapFileErrorText] = localizationTestText,
			[StringId.LoadMapAction] = localizationTestText,
			[StringId.AboutAppAction] = localizationTestText,
			[StringId.CheckForUpdatesAction] = localizationTestText,
			[StringId.LanguageSection] = localizationTestText,
			[StringId.PlayModeTitle] = localizationTestText,
			[StringId.FinishReachedText] = localizationTestText,
			[StringId.MapLoadingStatus] = "\\(TOTAL_MAPS): +\\(SUCCESSFUL_MAPS), -\\(FAILED_MAPS)",
			[StringId.MapLoadedSuccessfullyText] = "+ \\(MAP)",
			[StringId.MapLoadingFailedText] = "- \\(MAP): \\(ERROR_MESSAGE)",
			[StringId.DescriptionSection] = localizationTestText,
			[StringId.DescriptionText] = "\\(WEBSITE_LINK)",
			[StringId.LicenseSection] = localizationTestText,
			[StringId.VersionText] = "\\(COMPILATION_TYPE) \\(BUILD_TYPE) v\\(VERSION)",
			[StringId.OkAction] = "👍",
			[StringId.UpdateCheckTitle] = localizationTestText,
			[StringId.LatestVersionInfoText] = "\\(VERSION)",
			[StringId.CurrentVersionInfoText] = "\\(VERSION)",
			[StringId.SeeUpdateAction] = localizationTestText,
			[StringId.CloseAction] = localizationTestText,
			[StringId.CheckingForUpdatesStatus] = localizationTestText,
			[StringId.ErrorStatus] = localizationTestText,
			[StringId.UpdateAvailableStatus] = localizationTestText,
			[StringId.NoUpdateAvailableStatus] = localizationTestText,
			[StringId.CheckingForUpdatesStatusDetails] = localizationTestText,
			[StringId.ErrorStatusDetails] = "\\(ERROR_MESSAGE)",
			[StringId.UpdateAvailableStatusDetails] = localizationTestText,
			[StringId.NoUpdateAvailableStatusDetails] = localizationTestText
		}
#endif
	};

	static Localization()
	{
		StringById = localizationByLanguageId[defaultLanguageId];
		CurrentLanguage = availableLanguages.Find(language => language.LanguageId == defaultLanguageId);
	}

	public static void SetCurrentLanguage(string languageId)
	{
		if (!localizationByLanguageId.TryGetValue(languageId, out Dictionary<StringId, string>? localization)) throw new KeyNotFoundException($"Language with ID '{languageId}' is not available.");

		StringById = localization;
		CurrentLanguage = availableLanguages.Find(language => language.LanguageId == languageId);

		LocalizationChanged?.Invoke(null, EventArgs.Empty);
	}

	public static void LoadLanguage(XDocument xml)
	{
		XElement rootElement = xml.Root ?? throw new FormatException("XML document is empty.");

		XAttribute versionAttribute = rootElement.Attribute("Version") ?? throw new FormatException("'Version' attribute is not found.");

		if (!Version.TryParse(versionAttribute.Value.Trim(), out Version? version))
			throw new FormatException($"'Version' attribute is invalid (expected version in 'X.Y.Z' format, got '{versionAttribute.Value}').");

		if (new Version(Program.Version.Major, Program.Version.Minor, Program.Version.Build) != new Version(version.Major, version.Minor, version.Build))
			throw new NotSupportedException($"The specified translation is made for another PWSandbox version ('{version.ToString(3)}').");

		XElement
			metaElement = rootElement.Element("Meta") ?? throw new FormatException("'Meta' section is not found."),
			stringsElement = rootElement.Element("Strings") ?? throw new FormatException("'Strings' section is not found.");

		XElement
			languageIdElement = metaElement.Element("LanguageId") ?? throw new FormatException("'LanguageId' value is not found."),
			internationalNameElement = metaElement.Element("InternationalName") ?? throw new FormatException("'InternationalName' value is not found."),
			selfNameElement = metaElement.Element("SelfName") ?? throw new FormatException("'SelfName' value is not found."),
			isRightToLeftElement = metaElement.Element("IsRightToLeft") ?? throw new FormatException("'IsRightToLeft' value is not found.");

		if (!bool.TryParse(isRightToLeftElement.Value.Trim(), out bool isRightToLeft))
			throw new FormatException($"'IsRightToLeft' value is invalid (expected 'true' or 'false', got '{isRightToLeftElement.Value}').");

		Language language = new()
		{
			LanguageId = languageIdElement.Value.Trim(),
			InternationalName = internationalNameElement.Value.Trim(),
			SelfName = selfNameElement.Value.Trim(),
			IsRightToLeft = isRightToLeft
		};

		Dictionary<StringId, string> localization = [];
		foreach (StringId stringId in Enum.GetValues<StringId>())
		{
			XElement? translation = stringsElement.Element($"_{stringId}");

			if (translation is null)
			{
				localization[stringId] = localizationByLanguageId[defaultLanguageId][stringId];
				continue;
			}

			string[] translationLines = translation.Value.Trim().Split('\n');
			for (int i = 0; i < translationLines.Length; i++) translationLines[i] = translationLines[i].Trim();
			localization[stringId] = string.Join('\n', translationLines);
		}

		if (!localizationByLanguageId.TryAdd(language.LanguageId, localization)) throw new Exception($"Language with ID '{language.LanguageId}' already exists.");
		availableLanguages.Add(language);
	}
}
