// https://pws.yarb00.dev

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PWSandbox;

internal readonly record struct Position(int X, int Y);

internal sealed partial class PlayForm : Form
{
	private const int baseDpi = 96;
	private const int baseCellSize = 16;

	private static readonly Dictionary<MapObject, Color> ColorByMapObject = new()
	{
		[MapObject.Unknown] = Color.Magenta,
		[MapObject.Void] = Color.Gray,
		[MapObject.Player] = Color.Yellow,
		[MapObject.Finish] = Color.Green,
		[MapObject.Wall] = Color.Silver,
		[MapObject.FakeWall] = Color.Silver, // Same color as Wall
		[MapObject.Barrier] = Color.Gray // Same color as Void
	};

	private readonly MapObject[,] mapObjects;

	private Position? playerPosition = null, lastFinish = null;

	private int cellSize;

	public PlayForm(Map map)
	{
		mapObjects = map.Objects;

		InitializeComponent();
		CalculateCellSize();
		CalculateWindowSize();

		ApplyLocalization();
		Localization.LocalizationChanged += (_, _) => ApplyLocalization();
	}

	private void CalculateCellSize() => cellSize = (int)float.Ceiling(baseCellSize * ((float)DeviceDpi / baseDpi));

	private void CalculateWindowSize() => ClientSize = new Size(mapObjects.GetLength(1) * cellSize, mapObjects.GetLength(0) * cellSize);

	private void UpdateScaling(object sender, DpiChangedEventArgs e)
	{
		CalculateCellSize();
		CalculateWindowSize();
		Invalidate();
	}

	private void OnKeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape) Close();

		if (playerPosition is null) return;
		(int playerX, int playerY) = (Position)playerPosition;

		switch (e.KeyCode)
		{
			case Keys.W or Keys.Up:
				if (!IsCollision(playerX, playerY - 1)) playerY -= 1;
				break;

			case Keys.S or Keys.Down:
				if (!IsCollision(playerX, playerY + 1)) playerY += 1;
				break;

			case Keys.A or Keys.Left:
				if (!IsCollision(playerX - 1, playerY)) playerX -= 1;
				break;

			case Keys.D or Keys.Right:
				if (!IsCollision(playerX + 1, playerY)) playerX += 1;
				break;

			default:
				return;
		}

		playerPosition = new(playerX, playerY);

		Invalidate();
	}

	private void ProcessMap(object sender, PaintEventArgs e)
	{
		for (int y = 0; y < mapObjects.GetLength(0); y++)
		{
			for (int x = 0; x < mapObjects.GetLength(1); x++)
			{
				switch (mapObjects[y, x])
				{
					case MapObject.Player:
						playerPosition ??= new(x, y);
						DrawCell(e.Graphics, new(x, y), ColorByMapObject[MapObject.Void]);
						break;

					case MapObject.Finish when playerPosition == new Position(x, y) && lastFinish != new Position(x, y):
						MessageBox.Show(
							this,
							Localization.StringById[StringId.FinishReachedText],
							Localization.StringById[StringId.PlayModeTitle],
							MessageBoxButtons.OK,
							MessageBoxIcon.Information,
							MessageBoxDefaultButton.Button1,
							Localization.AreStringsRightToLeft ? MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading : 0
						);
						lastFinish = playerPosition;
						goto default;

					default:
						DrawCell(e.Graphics, new(x, y), ColorByMapObject[mapObjects[y, x]]);
						break;
				}
			}
		}

		if (playerPosition is null) return;
		DrawCell(e.Graphics, (Position)playerPosition, ColorByMapObject[MapObject.Player]);
	}

	private void DrawCell(Graphics graphics, Position coordinates, Color color)
	{
		using SolidBrush brush = new(color);

		graphics.FillRectangle(
			brush,
			x: coordinates.X * cellSize, y: coordinates.Y * cellSize,
			width: cellSize, height: cellSize
		);
	}

	private bool IsCollision(int x, int y) => IsCollision(new(x, y));

	private bool IsCollision(Position coordinates)
	{
		MapObject @object;
		try
		{
			@object = mapObjects[coordinates.Y, coordinates.X];
		}
		catch (IndexOutOfRangeException)
		{
			return true;
		}

		return @object is MapObject.Wall or MapObject.Barrier;
	}

	private void ApplyLocalization()
	{
		RightToLeft = RightToLeft.No; // RightToLeft should always be No because the map itself will get inverted (including the controls) if it's set to Yes
		RightToLeftLayout = Localization.AreStringsRightToLeft;

		Text = Localization.StringById[StringId.PlayModeTitle];
	}
}
