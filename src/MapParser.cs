// https://pws.yarb00.dev

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PWSandbox;

internal enum MapVersion
{
	V1_0,
	V1_1 // Added '.' as alias to ' ' ("Void" object)
}

internal enum MapObject
{
	Unknown = -1, Void,
	Player,
	Finish,
	Wall, FakeWall, Barrier
}

internal readonly record struct Map(MapObject[,] Objects);

internal static class MapParser
{
	/// <summary>
	/// Reads a file, parses its contents and creates a new <see cref="Map" /> object from it.
	/// </summary>
	/// <param name="filePath">The path to the file.</param>
	/// <param name="mapVersion">The map's version or <see langword="null" /> if it is unknown.</param>
	/// <returns>A <see cref="Map" /> parsed from the file.</returns>
	/// <exception cref="FormatException">Thrown when the provided map is empty, its version could not be detected or it is incorrect and cannot be parsed.</exception>
	/// <exception cref="NotSupportedException">Thrown when the version of the provided map is not supported.</exception>
	public static Map ParseMapFromFile(string filePath, MapVersion? mapVersion = null)
	{
		try
		{
			string[] mapLines = File.ReadAllLines(filePath, Encoding.UTF8);
			return ParseMapFromStringArray(mapLines, mapVersion);
		}
		catch
		{
			throw;
		}
	}

	/// <summary>
	/// Parses the provided map and creates a new <see cref="Map" /> object from it.
	/// </summary>
	/// <param name="mapLines">The map's contents.</param>
	/// <param name="mapVersion">The map's version or <see langword="null" /> if it is unknown.</param>
	/// <returns>A <see cref="Map" /> parsed from <paramref name="mapLines" />.</returns>
	/// <exception cref="FormatException">Thrown when the provided map is empty, its version could not be detected or it is incorrect and cannot be parsed.</exception>
	/// <exception cref="NotSupportedException">Thrown when the version of the provided map is not supported.</exception>
	public static Map ParseMapFromStringArray(string[] mapLines, MapVersion? mapVersion = null)
	{
		if (mapLines.Length == 0) throw new FormatException("Map cannot be empty.");

		if (mapVersion is null)
		{
			try
			{
				mapVersion = DetectMapVersionFromStringArray(mapLines);
			}
			catch
			{
				throw;
			}
		}

		try
		{
			return mapVersion switch
			{
				MapVersion.V1_0 => ParseMapV1_0(mapLines),
				MapVersion.V1_1 => ParseMapV1_1(mapLines),
				_ => throw new NotSupportedException($"Map version '{mapVersion}' is not supported by this version of PWSandbox.")
			};
		}
		catch
		{
			throw;
		}
	}

	/// <summary>
	/// Detects the version of the provided map.
	/// </summary>
	/// <remarks>
	/// Does not guarantee that the map is correct and can be parsed.
	/// </remarks>
	/// <param name="mapLines">The map's contents.</param>
	/// <returns>A <see cref="MapVersion" /> indicating the map's version.</returns>
	/// <exception cref="FormatException">Thrown when the provided map is empty or its version could not be detected.</exception>
	public static MapVersion DetectMapVersionFromStringArray(string[] mapLines)
	{
		if (mapLines.Length == 0) throw new FormatException("Map cannot be empty.");

		if (mapLines[0].TrimStart().StartsWith("?PWSandbox-Map 1.0;", true, null)) return MapVersion.V1_0;
		else if (mapLines[0].TrimStart().StartsWith("?PWSandbox-Map 1.1;", true, null)) return MapVersion.V1_1;
		else throw new FormatException("Failed to detect map version.");
	}

	#region Parsers

	private static Map ParseMapV1_0(string[] mapLines) => ParseMapV1_1(mapLines, legacyBehavior: true);

	private static Map ParseMapV1_1(string[] mapLines, bool legacyBehavior = false)
	{
		for (int y = 0; y < 3; y++)
		{
			string mapHeader = legacyBehavior ? "?PWSandbox-Map 1.0;" : "?PWSandbox-Map 1.1;";

			mapLines = mapLines.Where(@string => !string.IsNullOrWhiteSpace(@string)).ToArray();

			if (mapLines.Length == 0) throw new FormatException($"Map cannot be empty.");

			switch (y)
			{
				case 0:
					if (mapLines[0].TrimStart().StartsWith(mapHeader, true, null))
					{
						mapLines[0] = mapLines[0].TrimStart().Remove(0, mapHeader.Length);
						continue;
					}
					else throw new FormatException($"Map header ('{mapHeader}') was not found.");

				case 1:
					if (mapLines[0].TrimStart().StartsWith("(map: begin)", true, null))
					{
						mapLines[0] = mapLines[0].TrimStart().Remove(0, "(map: begin)".Length);
						continue;
					}
					else throw new FormatException($"Expected '(map: begin)' after map header ('{mapHeader}'), but it was not found.");

				case 2:
					if (mapLines[^1].TrimEnd().EndsWith("(map: end)", true, null))
					{
						mapLines[^1] = mapLines[^1].TrimEnd().Remove(mapLines[^1].Length - "(map: end)".Length, "(map: end)".Length);
						mapLines = mapLines.Where(@string => !string.IsNullOrWhiteSpace(@string)).ToArray();
						break;
					}
					else throw new FormatException($"Expected '(map: end)' in the end, but it was not found.");
			}
		}

		int maxX = 0;
		for (int y = 0; y < mapLines.Length; y++)
			if (maxX < mapLines[y].Length)
				maxX = mapLines[y].Length;

		MapObject[,] mapObjects = new MapObject[mapLines.Length, maxX];

		for (int y = 0; y < mapLines.Length; y++)
		{
			for (int x = 0; x < mapLines[y].Length; x++)
			{
				mapObjects[y, x] = mapLines[y][x] switch
				{
					' ' => MapObject.Void,
					'.' when !legacyBehavior => MapObject.Void,
					'!' => MapObject.Player,
					'=' => MapObject.Finish,
					'@' => MapObject.Wall,
					'#' => MapObject.FakeWall,
					'*' => MapObject.Barrier,
					_ => MapObject.Unknown
				};
			}
		}

		return new Map(mapObjects);
	}

	#endregion
}
