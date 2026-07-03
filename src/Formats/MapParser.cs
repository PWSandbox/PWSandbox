// https://pws.yarb00.dev

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PWSandbox.Formats;

/// <summary>
/// The version of the PWSandbox Map Format used by a map.
/// </summary>
public enum MapVersion
{
	/// <summary>
	/// Version 1.0.
	/// </summary>
	V1_0,

	/// <summary>
	/// Version 1.1.
	/// </summary>
	/// <remarks>
	/// Added <c>'.'</c> as an alias for <c>' '</c> (the "Void" object).
	/// </remarks>
	V1_1,
}

/// <summary>
/// An object placed on a <see cref="Map" />.
/// </summary>
public enum MapObject
{
	/// <summary>
	/// The "Unknown" object.
	/// Used as a placeholder for all unrecognized objects encountered while parsing a <see cref="Map" />.
	/// </summary>
	Unknown = -1,

	/// <summary>
	/// The "Void" object.
	/// </summary>
	Void,

	/// <summary>
	/// The "Player" object.
	/// </summary>
	Player,

	/// <summary>
	/// The "Finish" object.
	/// </summary>
	Finish,

	/// <summary>
	/// The "Wall" object.
	/// </summary>
	Wall,

	/// <summary>
	/// The "FakeWall" object.
	/// </summary>
	FakeWall,

	/// <summary>
	/// The "Barrier" object.
	/// </summary>
	Barrier,
}

/// <summary>
/// A PWSandbox map.
/// </summary>
/// <param name="Objects">The objects placed on a map.</param>
public readonly record struct Map(MapObject[,] Objects)
{
	/// <summary>
	/// Read a file, parse its content and create a new <see cref="Map" /> instance from it.
	/// </summary>
	/// <param name="path">The path to map file.</param>
	/// <param name="mapVersion">The map's version or <see langword="null" /> if it is unknown to try detecting it.</param>
	/// <returns>A <see cref="Map" /> parsed from the file.</returns>
	/// <exception cref="FormatException">Thrown when the provided map is empty, its version could not be detected or it is incorrect and can't be parsed.</exception>
	/// <exception cref="NotSupportedException">Thrown when the version of the provided map is not supported.</exception>
	public static Map ParseFromFile(string path, MapVersion? mapVersion = null) =>
		ParseFromStringArray(mapLines: File.ReadAllLines(path, Encoding.UTF8), mapVersion);

	/// <summary>
	/// Parse the provided map and create a new <see cref="Map" /> instance from it.
	/// </summary>
	/// <param name="mapLines">The serialized map, split by line breaks.</param>
	/// <param name="mapVersion">The map's version or <see langword="null" /> if it is unknown to try detecting it.</param>
	/// <returns>A <see cref="Map" /> parsed from <paramref name="mapLines" />.</returns>
	/// <exception cref="FormatException">Thrown when the provided map is empty, its version could not be detected or it is incorrect and can't be parsed.</exception>
	/// <exception cref="NotSupportedException">Thrown when the version of the provided map is not supported.</exception>
	public static Map ParseFromStringArray(string[] mapLines, MapVersion? mapVersion = null)
	{
		if (mapLines.Length == 0)
			throw new FormatException("Map cannot be empty.");

		mapVersion ??= DetectMapVersionFromStringArray(mapLines);

		return mapVersion switch
		{
			MapVersion.V1_0 => MapParsers.ParseMapV1_0(mapLines),
			MapVersion.V1_1 => MapParsers.ParseMapV1_1(mapLines),
			_ => throw new NotSupportedException(
				$"Map version '{mapVersion}' is not supported by this version of PWSandbox."
			),
		};
	}

	/// <summary>
	/// Detect the version of the provided map.
	/// </summary>
	/// <remarks>
	/// Successful version detection does not guarantee that the map is correct and can be parsed.
	/// </remarks>
	/// <param name="mapLines">The serialized map, split by line breaks.</param>
	/// <returns>A <see cref="MapVersion" /> indicating the map's version.</returns>
	/// <exception cref="FormatException">Thrown when the provided map is empty or its version could not be detected.</exception>
	public static MapVersion DetectMapVersionFromStringArray(string[] mapLines)
	{
		if (mapLines.Length == 0)
			throw new FormatException("Map cannot be empty.");

		if (
			mapLines[0]
				.TrimStart()
				.StartsWith("?PWSandbox-Map 1.0;", StringComparison.OrdinalIgnoreCase)
		)
			return MapVersion.V1_0;
		else if (
			mapLines[0]
				.TrimStart()
				.StartsWith("?PWSandbox-Map 1.1;", StringComparison.OrdinalIgnoreCase)
		)
			return MapVersion.V1_1;
		else
			throw new FormatException("Failed to detect map version.");
	}
}

internal static class MapParsers
{
	public static Map ParseMapV1_0(string[] mapLines) =>
		ParseMapV1_1(mapLines, legacyBehavior: true);

	public static Map ParseMapV1_1(string[] mapLines, bool legacyBehavior = false)
	{
		for (int y = 0; y < 3; y++)
		{
			string mapHeader = legacyBehavior ? "?PWSandbox-Map 1.0;" : "?PWSandbox-Map 1.1;";

			mapLines = mapLines.Where(@string => !string.IsNullOrWhiteSpace(@string)).ToArray();

			if (mapLines.Length == 0)
				throw new FormatException($"Map cannot be empty.");

			switch (y)
			{
				case 0:
					if (
						!mapLines[0]
							.TrimStart()
							.StartsWith(mapHeader, StringComparison.OrdinalIgnoreCase)
					)
						throw new FormatException($"The map header ('{mapHeader}') was not found.");
					mapLines[0] = mapLines[0].TrimStart()[mapHeader.Length..];
					continue;

				case 1:
					if (
						!mapLines[0]
							.TrimStart()
							.StartsWith("(map: begin)", StringComparison.OrdinalIgnoreCase)
					)
						throw new FormatException(
							$"Expected the '(map: begin)' block after the map header ('{mapHeader}'), but it was not found."
						);
					mapLines[0] = mapLines[0].TrimStart()["(map: begin)".Length..];
					continue;

				case 2:
					if (
						!mapLines[^1]
							.TrimEnd()
							.EndsWith("(map: end)", StringComparison.OrdinalIgnoreCase)
					)
						throw new FormatException(
							$"Expected the '(map: end)' block at the end of the map, but it was not found."
						);
					mapLines[^1] = mapLines[^1]
						.TrimEnd()
						.Remove(mapLines[^1].Length - "(map: end)".Length, "(map: end)".Length);
					mapLines = mapLines
						.Where(@string => !string.IsNullOrWhiteSpace(@string))
						.ToArray();
					break;
			}
		}

		int maxX = 0;
		foreach (string line in mapLines)
			if (maxX < line.Length)
				maxX = line.Length;

		MapObject[,] mapObjects = new MapObject[mapLines.Length, maxX];

		for (int y = 0; y < mapLines.Length; y++)
		{
			for (int x = 0; x < mapLines[y].Length; x++)
			{
				mapObjects[y, x] = mapLines[y][x] switch
				{
					'.' when !legacyBehavior => MapObject.Void,
					' ' => MapObject.Void,
					'!' => MapObject.Player,
					'=' => MapObject.Finish,
					'@' => MapObject.Wall,
					'#' => MapObject.FakeWall,
					'*' => MapObject.Barrier,
					_ => MapObject.Unknown,
				};
			}
		}

		return new Map(mapObjects);
	}
}
