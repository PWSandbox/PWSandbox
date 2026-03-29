// https://pws.yarb00.dev

using System;
using System.Collections.Generic;

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
	public static Dictionary<StringId, string> StringById { get; private set; } = LocalizationByLanguageId["en"]; // Mapping of StringId to text for the current language

	public static List<Language> AvailableLanguages { get; private set; } = [
		new Language
		{
			LanguageId = "en",
			InternationalName = "English",
			SelfName = "English",
			IsRightToLeft = false
		},
		new Language
		{
			LanguageId = "ru",
			InternationalName = "Russian",
			SelfName = "Русский",
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

	public static event EventHandler? LocalizationChanged;

	public static bool AreStringsRightToLeft { get; private set; } = false; // Default language is English

	private static Dictionary<string, Dictionary<StringId, string>> LocalizationByLanguageId => new() // Mappings of StringId to text for every available language
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
			[StringId.VersionText] = "Version \\(VERSION), \\(BUILD_TYPE) build",
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
		["ru"] = new()
		{
			[StringId.UnhandledExceptionTitle] = "PWSandbox: Необработанное исключение...",
			[StringId.UnhandledExceptionResumingNotAllowedText] = """
				Возникло необработанное исключение (ошибка).

				\(REPORT_LINK_START)[Сообщить об ошибке]\(REPORT_LINK_END)

				Нажмите кнопку "Закрыть", чтобы выйти из PWSandbox.
				""",
			[StringId.UnhandledExceptionResumingAllowedText] = """
				Возникло необработанное исключение (ошибка).
				
				\(REPORT_LINK_START)[Сообщить об ошибке]\(REPORT_LINK_END)

				Нажмите кнопку "Закрыть", чтобы выйти из PWSandbox.
				Вы также можете нажать кнопку "Продолжить", чтобы попытаться проигнорировать ошибку и вернуться в PWSandbox.
				Однако нет гарантий, что всё будет работать стабильно или вообще будет работать.
				""",
			[StringId.ShowDetailsAction] = "Показать подробности",
			[StringId.HideDetailsAction] = "Скрыть подробности",
			[StringId.DetailsSection] = "===== Подробнее: =====",
			[StringId.MapFileSelectionTitle] = "Выберите карту PWSandbox...",
			[StringId.MapFileErrorTitle] = "Карта PWSandbox: Ошибка!",
			[StringId.MapFileErrorText] = """
				Не удалось загрузить карту.
				Либо она предназначена для более новой версии PWSandbox, либо содержит ошибки.

				Сообщите создателю карты об этой проблеме.
				Если карту создали вы и пытаетесь загрузить её в поддерживаемой версии PWSandbox,
				остаётся только одно: вы допустили ошибку при написании карты. Подробности ниже.
				""",
			[StringId.LoadMapAction] = "Открыть карту...",
			[StringId.AboutAppAction] = "О программе",
			[StringId.CheckForUpdatesAction] = "Проверить наличие обновлений",
			[StringId.LanguageSection] = "Язык PWSandbox:",
			[StringId.PlayModeTitle] = "Карта PWSandbox",
			[StringId.FinishReachedText] = "Вы пришли к финишу!",
			[StringId.MapLoadingStatus] = """
				Из \(TOTAL_MAPS) карт:
				\(SUCCESSFUL_MAPS) успешно загрузились,
				и \(FAILED_MAPS) из них PWSandbox не смог загрузить.
				""",
			[StringId.MapLoadedSuccessfullyText] = """
				Карта "\(MAP)"
				успешно загружена.
				""",
			[StringId.MapLoadingFailedText] = """
				Во время загрузки карты "\(MAP)" произошла ошибка:
				\(ERROR_MESSAGE)
				""",
			[StringId.DescriptionSection] = "Описание:",
			[StringId.DescriptionText] = """
				Игра-песочница, написанная на C# с использованием Windows Forms.

				Сайт: \(WEBSITE_LINK)
				""",
			[StringId.LicenseSection] = "Текст лицензии (на английском):",
			[StringId.VersionText] = "Версия \\(VERSION) (\\(BUILD_TYPE))",
			[StringId.OkAction] = "OK",
			[StringId.UpdateCheckTitle] = "Проверка обновлений PWSandbox",
			[StringId.LatestVersionInfoText] = "Последняя версия: \\(VERSION)",
			[StringId.CurrentVersionInfoText] = "Установленная версия: \\(VERSION)",
			[StringId.SeeUpdateAction] = "Открыть страницу обновления",
			[StringId.CloseAction] = "Закрыть",
			[StringId.CheckingForUpdatesStatus] = "Проверка наличия обновлений...",
			[StringId.ErrorStatus] = "Ошибка...",
			[StringId.UpdateAvailableStatus] = "Доступно обновление...",
			[StringId.NoUpdateAvailableStatus] = "Обновлений нет...",
			[StringId.CheckingForUpdatesStatusDetails] = "Получение данных с сервера...",
			[StringId.ErrorStatusDetails] = """
				Во время проверки обновлений произошла ошибка:

				\(ERROR_MESSAGE)
				""",
			[StringId.UpdateAvailableStatusDetails] = "Чтобы узнать больше о последнем обновлении PWSandbox, выберите \"Открыть страницу обновления\".",
			[StringId.NoUpdateAvailableStatusDetails] = "Ура! У вас актуальная версия."
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
			[StringId.VersionText] = "\\(BUILD_TYPE) v\\(VERSION)",
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

#if DEBUG
	private const string localizationTestText = "أنا اجيب أن أبي. أبي هو يعمل كثيرا و يساعدوني في حياتي. أبي هو لطيف و محبوب. هو يحب أن يساعد الناس هو لا يسكن في بيت.";
#endif

	public static void SetLanguage(string languageId)
	{
		if (!LocalizationByLanguageId.TryGetValue(languageId, out Dictionary<StringId, string>? localization)) throw new KeyNotFoundException($"Language '{languageId}' is not available.");

		StringById = localization;
		AreStringsRightToLeft = AvailableLanguages.Find(language => language.LanguageId == languageId).IsRightToLeft;

		LocalizationChanged?.Invoke(null, EventArgs.Empty);
	}
}
