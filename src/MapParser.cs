// https://pws.yarb00.dev

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace PWSandbox;

internal enum MapVersion
{
	V1_0,
	V1_1, // Added '.' as alias to ' ' (the "Void" object)
	V2_X // XML-based maps (not supported)
}

internal enum MapObject
{
	Unknown = -1, Void,
	Player,
	Finish,
	Wall, FakeWall, Barrier
}

internal static class MapParser
{
	public static MapObject[,] ParseMapFromFile(string filePath, MapVersion? mapVersion = null)
	{
		try
		{
			return ParseMapFromStringArray(File.ReadAllLines(filePath), mapVersion);
		}
		catch (ArgumentException e) when (e.ParamName == "mapLines")
		{
			throw new FileFormatException("Provided file is empty.", e);
		}
		catch
		{
			throw;
		}
	}

	public static MapObject[,] ParseMapFromStringArray(string[] mapLines, MapVersion? mapVersion = null)
	{
		if (mapLines.Length == 0) throw new ArgumentException("Map cannot be empty.", nameof(mapLines));

		if (mapVersion is null)
		{
			if (IsXmlBasedMap(string.Join(Environment.NewLine, mapLines)))
				mapVersion = MapVersion.V2_X;
			else if (mapLines[0].TrimStart().StartsWith("?PWSandbox-Map 1.0;", true, null))
				mapVersion = MapVersion.V1_0;
			else if (mapLines[0].TrimStart().StartsWith("?PWSandbox-Map 1.1;", true, null))
				mapVersion = MapVersion.V1_1;
			else throw new NotSupportedException("Failed to detect map version.");
		}

		try
		{
			return mapVersion switch
			{
				MapVersion.V1_0 => ParseMapV1_0(mapLines),
				MapVersion.V1_1 => ParseMapV1_1(mapLines),
				MapVersion.V2_X => throw new NotSupportedException("XML-based maps are not supported in this version of PWSandbox."),
				_ => throw new UnreachableException()
			};
		}
		catch
		{
			throw;
		}
	}

	private static bool IsXmlBasedMap(string mapText)
	{
		try
		{
			new XmlDocument().Load(mapText);
			return true;
		}
		catch
		{
			return false;
		}
	}

	#region Parsers

	private static MapObject[,] ParseMapV1_0(string[] mapLines) => ParseMapV1_1(mapLines, true);

	private static MapObject[,] ParseMapV1_1(string[] mapLines, bool legacyBehavior = false)
	{
		for (int y = 0; y < 3; y++)
		{
			string mapHeader = legacyBehavior ? "?PWSandbox-Map 1.0;" : "?PWSandbox-Map 1.1;";

			mapLines = mapLines.Where(static str => !string.IsNullOrWhiteSpace(str)).ToArray();

			switch (y)
			{
				case 0:
					if (mapLines[0].TrimStart().StartsWith(mapHeader, true, null))
					{
						mapLines[0] = mapLines[0].TrimStart()[mapHeader.Length..];
						continue;
					}
					else throw new FormatException($"Map header with a supported version of standard was not found.");

				case 1:
					if (mapLines[0].TrimStart().StartsWith("(map: begin)", true, null))
					{
						mapLines[0] = mapLines[0].TrimStart()["(map: begin)".Length..];
						continue;
					}
					else throw new FormatException($"Expected \"(map: begin)\" block after the map header (\"{mapHeader}\"), but it was not found.");

				case 2:
					if (mapLines[^1].TrimEnd().EndsWith("(map: end)", true, null))
					{
						mapLines[^1] = mapLines[^1].TrimEnd().Remove(mapLines[^1].Length - "(map: end)".Length, "(map: end)".Length);
						mapLines = mapLines.Where(static str => !string.IsNullOrWhiteSpace(str)).ToArray();

						break;
					}
					else throw new FormatException("Expected \"(map: end)\" block in the end of the map, but it was not found.");
			}
		}

		int maxX = 0;
		for (int y = 0; y < mapLines.Length; y++)
			if (maxX < mapLines[y].Length)
				maxX = mapLines[y].Length;

		MapObject[,] mapObjects = new MapObject[mapLines.Length, maxX];

		for (int y = 0; y < mapLines.Length; y++)
			for (int x = 0; x < mapLines[y].Length; x++)
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

		return mapObjects;
	}

	#endregion
}
