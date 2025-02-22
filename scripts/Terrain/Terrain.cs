using Godot;
using System;
using System.Collections.Generic;

public partial class Terrain : TileMapLayer
{
	public bool Timer = true;
	[Export]
	public bool AnimateForwards = true;
	private Dictionary<string, int> _sourceIdDict = new Dictionary<string, int>();

	public override void _Ready()
	{
		base._Ready();
		// Add/connect timer timeout signal
		Timer myTimer = GetNode<Timer>("Timer");
		myTimer.Timeout += () => OnTimerTimeout();

		// Setup sourceIdDict
		for (int i = 0; i < TileSet.GetSourceCount(); i++)
		{
			TileSetSource source = TileSet.GetSource(i);
			_sourceIdDict[source.ResourceName] = i;
		}
	}

	/// <summary>
	/// Gets the source id that is relative to the tilemap resource name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	private int _GetSourceIdByName(string name)
	{
		if (_sourceIdDict.TryGetValue(name, out int value))
			return value;
		else
			return -1;
	}

	/// <summary>
	/// Gets the source object, relative to the tilemap resource name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	private TileSetSource _GetSourceByName(string name)
	{
		return TileSet.GetSource(_GetSourceIdByName(name));
	}

	/// <summary>
	/// Gets all used active tiles, from a certain tilemap. relative to the given resource name.
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	private Godot.Collections.Array<Vector2I> _GetUsedCellsByName(string name)
	{
		return GetUsedCellsById(_GetSourceIdByName(name));
	}

	public override void _Process(double delta)
	{
		if (Timer)
		{
			foreach (var cell in _GetUsedCellsByName("RedGradient"))
			{
				Vector2I tile = GetCellAtlasCoords(cell);
				if (tile.X >= 4 && AnimateForwards)
					AnimateForwards = false;
				if (tile.X <= 0 && !AnimateForwards)
					AnimateForwards = true;
				GD.Print(tile);
				if (AnimateForwards)
					SetCell(cell, 5, new Vector2I(tile.X + 1, tile.Y));
				else
					SetCell(cell, 5, new Vector2I(tile.X - 1, tile.Y));
			}
			Timer = false;
		}
	}

	public void OnTimerTimeout()
	{
		Timer = true;
	}
}
