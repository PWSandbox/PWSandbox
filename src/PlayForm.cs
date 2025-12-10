// https://pws.yarb00.dev

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PWSandbox;

internal partial class PlayForm : Form
{
	private const int baseDpi = 96;
	private const int baseCellSize = 26;

	private int CellSize => (int)float.Ceiling(baseCellSize * ((float)DeviceDpi / baseDpi));

	private readonly MapObject[,] mapObjects;

	private (int x, int y)? playerPosition = null, lastFinish = null;

	private readonly Dictionary<MapObject, Color> ColorByMapObject = new()
	{
		[MapObject.Unknown] = Color.Magenta,
		[MapObject.Void] = Color.Gray,
		[MapObject.Player] = Color.Yellow,
		[MapObject.Finish] = Color.Green,
		[MapObject.Wall] = Color.Silver,
		[MapObject.FakeWall] = Color.Silver, // Same color as Wall
		[MapObject.Barrier] = Color.Gray // Same color as Void
	};

	public PlayForm(MapObject[,] mapObjects, Dictionary<MapObject, Color>? colorByMapObject = null)
	{
		InitializeComponent();

		this.mapObjects = mapObjects;

		ClientSize = new Size(mapObjects.GetLength(1) * CellSize, mapObjects.GetLength(0) * CellSize);

		if (colorByMapObject is null) return;

		foreach (var color in colorByMapObject) ColorByMapObject[color.Key] = color.Value;
		BackColor = ColorByMapObject[MapObject.Void];
	}

	private void OnKeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape) Close();

		if (playerPosition is null ||
			!new List<Keys>
			{
				Keys.W, Keys.Up,
				Keys.S, Keys.Down,
				Keys.A, Keys.Left,
				Keys.D, Keys.Right
			}.Contains(e.KeyCode)) return;

		(int playerX, int playerY) = ((int, int))playerPosition;

		switch (e.KeyCode)
		{
			case Keys.W or Keys.Up:
				if (!IsCollision((playerX, playerY - 1))) playerY -= 1;
				break;
			case Keys.S or Keys.Down:
				if (!IsCollision((playerX, playerY + 1))) playerY += 1;
				break;
			case Keys.A or Keys.Left:
				if (!IsCollision((playerX - 1, playerY))) playerX -= 1;
				break;
			case Keys.D or Keys.Right:
				if (!IsCollision((playerX + 1, playerY))) playerX += 1;
				break;
		}

		playerPosition = (playerX, playerY);

		Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);

		ProcessMap(e.Graphics);
	}

	private void ProcessMap(Graphics graphics)
	{
		for (int y = 0; y < mapObjects.GetLength(0); y++)
			for (int x = 0; x < mapObjects.GetLength(1); x++)
				switch (mapObjects[y, x])
				{
					case MapObject.Player:
						playerPosition ??= (x, y);
						DrawCell(graphics, (x, y), CellSize, ColorByMapObject[MapObject.Void]);
						break;

					case MapObject.Finish:
						if (playerPosition == (x, y) && lastFinish != (x, y))
						{
							MessageBox.Show(
								"You have reached the finish!",
								"PWSandbox [Play]",
								MessageBoxButtons.OK,
								MessageBoxIcon.Information,
								MessageBoxDefaultButton.Button1
							);

							lastFinish = playerPosition;
						}
						DrawCell(graphics, (x, y), CellSize, ColorByMapObject[MapObject.Finish]);
						break;

					default:
						DrawCell(graphics, (x, y), CellSize, ColorByMapObject[mapObjects[y, x]]);
						break;
				}

		if (playerPosition is null) return;
		DrawCell(graphics, ((int x, int y))playerPosition, CellSize, ColorByMapObject[MapObject.Player]);
	}

	private static void DrawCell(Graphics graphics, (int x, int y) coordinates, int cellSize, Color color)
	{
		using SolidBrush brush = new(color);

		graphics.FillRectangle(
			brush,
			coordinates.x * cellSize, coordinates.y * cellSize,
			cellSize, cellSize
		);
	}

	private bool IsCollision((int x, int y) coordinates)
	{
		try
		{
			if (mapObjects[coordinates.y, coordinates.x] is MapObject.Wall or MapObject.Barrier)
				return true;
		}
		catch (IndexOutOfRangeException)
		{
			return true;
		}

		return false;
	}
}
