// https://pws.yarb00.dev

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PWSandbox;

internal readonly record struct UpdateData(Version? LatestVersion, Uri? DetailsUrl);

internal static class Updater
{
	private const string updateDataLocation = $"{Program.Website}/update/data/client/release.pws-update-data.json";

	private static readonly HttpClient httpClient = new();

	static Updater()
	{
		httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
		httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"PWSandbox/{Program.FriendlyVersion} (+{Program.Website})");
	}

	public static bool? IsUpdateAvailable(UpdateData updateData) => updateData.LatestVersion is null ? null : IsUpdateAvailable(updateData.LatestVersion, Program.Version);

	public static bool IsUpdateAvailable(Version latestVersion, Version installedVersion) =>
		// Trim the 4th section of Version, since PWSandbox versions are in the A.B.C format
		new Version(latestVersion.Major, latestVersion.Minor, latestVersion.Build) > new Version(installedVersion.Major, installedVersion.Minor, installedVersion.Build);

	public static async Task<UpdateData> GetUpdateData()
	{
		string rawUpdateData = await httpClient.GetStringAsync(updateDataLocation);

		JsonElement updateData = JsonDocument.Parse(rawUpdateData, new JsonDocumentOptions
		{
			AllowTrailingCommas = true,
			CommentHandling = JsonCommentHandling.Skip
		}).RootElement;

		return new UpdateData
		{
			LatestVersion = GetLatestVersion(updateData),
			DetailsUrl = GetDetailsUrl(updateData)
		};
	}

	private static Version? GetLatestVersion(JsonElement updateData)
	{
		if (!updateData.TryGetProperty("latest_branch_version", out JsonElement latestBranchVersionElement)) return null;

		string? rawLatestVersion = latestBranchVersionElement.GetString();

		_ = Version.TryParse(rawLatestVersion, out Version? latestVersion);

		return latestVersion;
	}

	private static Uri? GetDetailsUrl(JsonElement updateData)
	{
		if (!updateData.TryGetProperty("latest_branch_version_info", out JsonElement latestBranchVersionInfoElement)) return null;

		string? rawDetailsUrl = latestBranchVersionInfoElement.GetString();

		_ = Uri.TryCreate(rawDetailsUrl, new UriCreationOptions(), out Uri? detailsUrl);

		return detailsUrl;
	}
}
