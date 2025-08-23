// PWSandbox ( https://github.com/PWSandbox/PWSandbox )
// Licensed under the MIT (Expat) license; Copyright (c) 2024-2025 yarb00

using Octokit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PWSandbox;

static class Updater
{
	private const string mainJsonUpdateInfoLocation = "https://pws.yarb00.dev/update/data/client/winforms.pws-update-data.json";
	private const string backupJsonUpdateInfoLocation = "https://gist.githubusercontent.com/yarb00/78c17da868cb352e50eb1639776eedbb/raw/winforms.pws-update-data.json";
	private const long githubRepositoryID = 906638888;

	private static readonly Version? currentVersion = Program.Version;

	public static async Task<(bool isUpdateAvailable, Version latestVersion, string releaseUrl)> GetUpdateInfo()
	{
		if (currentVersion is null) throw new NullReferenceException("App version is null; update checking is not possible");

		Version fetchedVersion;
		string updateUrl;

		try
		{
			(fetchedVersion, updateUrl) = await GetLatestVersion();
		}
		catch
		{
			throw;
		}

		Version
			latestVersionTrimmed = new(
				fetchedVersion.Major,
				fetchedVersion.Minor,
				fetchedVersion.Build,
				0
			),

			currentVersionTrimmed = new(
				currentVersion.Major,
				currentVersion.Minor,
				currentVersion.Build,
				0
			);

		if (currentVersionTrimmed.CompareTo(latestVersionTrimmed) < 0) // If the latest version is newer
			return (true, latestVersionTrimmed, updateUrl);
		else
			return (false, latestVersionTrimmed, updateUrl);
	}

	private static async Task<(Version fetchedVersion, string updateUrl)> GetLatestVersion()
	{
		try
		{
			return await GetLatestVersion_Json();
		}
		catch { }

		try
		{
			return await GetLatestVersion_Json(backupJsonUpdateInfoLocation);
		}
		catch { }

		try
		{
			return await GetLatestVersion_GitHub();
		}
		catch { }

		try
		{
			return await GetLatestVersion_GitHub("PWSandbox");
		}
		catch { }

		try
		{
			return await GetLatestVersion_GitHub("yarb00");
		}
		catch { }

		throw new Exception("All ways of update checking failed");
	}

	private static async Task<(Version fetchedVersion, string updateUrl)> GetLatestVersion_Json(string jsonLocation = mainJsonUpdateInfoLocation)
	{
		using HttpClient http = new();
		using HttpResponseMessage response = await http.GetAsync(jsonLocation);
		using JsonDocument json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());

		JsonElement root = json.RootElement;
		string latestBranchVersion = root.GetProperty("latest_branch_version").GetRawText();
		string latestBranchVersionInfo = root.GetProperty("latest_branch_version_info").GetRawText();

		Version fetchedVersion = Version.Parse(latestBranchVersion);

		return (fetchedVersion, latestBranchVersionInfo);
	}

	private static async Task<(Version fetchedVersion, string updateUrl)> GetLatestVersion_GitHub(long repositoryID = githubRepositoryID)
	{
		GitHubClient githubClient = GetLatestVersion_GitHub_SetupClient();

		IReadOnlyList<Release> releases;

		try
		{
			releases = await githubClient.Repository.Release.GetAll(repositoryID);
		}
		catch
		{
			throw;
		}

		return GetLatestVersion_GitHub_FromReleasesList(releases);
	}

	private static async Task<(Version fetchedVersion, string updateUrl)> GetLatestVersion_GitHub(string author, string name = "PWSandbox")
	{
		GitHubClient githubClient = GetLatestVersion_GitHub_SetupClient();

		IReadOnlyList<Release> releases;

		try
		{
			releases = await githubClient.Repository.Release.GetAll(author, name);
		}
		catch
		{
			throw;
		}

		return GetLatestVersion_GitHub_FromReleasesList(releases);
	}

	#region GitHub-specific

	private static GitHubClient GetLatestVersion_GitHub_SetupClient()
	{
		if (currentVersion is null) throw new NullReferenceException("App version is null; update checking is not possible");

		return new(new ProductHeaderValue("PWSandbox", currentVersion.ToString(3)));
	}

	private static (Version fetchedVersion, string updateUrl) GetLatestVersion_GitHub_FromReleasesList(IReadOnlyList<Release> list)
	{
		Release latestRelease = list[0];

		if (!latestRelease.TagName.StartsWith('v'))
			throw new FormatException("Tag name doesn't begin with \"v\"");

		Version fetchedVersion = Version.Parse(latestRelease.TagName.AsSpan(1));

		return (fetchedVersion, latestRelease.HtmlUrl);
	}

	#endregion
}
