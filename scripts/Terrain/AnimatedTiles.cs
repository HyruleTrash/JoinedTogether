using Godot;
using System;
using System.Collections.Generic;

public partial class AnimatedTiles : Node
{
    [Export]
    public float WaitTime = 0.2f;
    [Export]
    public bool Timer = true;
    private TileMapLayer _tileMapLayer;
    private Dictionary<string, int> _sourceIdDict = new Dictionary<string, int>();
    private Stack<AnimatedTileData> _animatedTileDatas = new Stack<AnimatedTileData>();

    public override void _Ready()
    {
        base._Ready();
        // Add/connect timer
        Timer myTimer = new Timer();
        myTimer.Name = "Timer";
        myTimer.WaitTime = WaitTime;
        myTimer.Autostart = true;
        myTimer.Timeout += () => OnTimerTimeout();
        AddChild(myTimer);

        _tileMapLayer = GetNode<TileMapLayer>("../");

        _SetupAnimatedTiles();
        _SetupSourceIdDictionary();
    }

    /// <summary>
    /// Fills the collection _animatedTileDatas, with the data needed to animate different tiles from tilemaps
    /// </summary>
    private void _SetupAnimatedTiles()
    {
        _animatedTileDatas.Push(new AnimatedTileData("RedGradient", 0, 4, true));
        _animatedTileDatas.Push(new AnimatedTileData("Moons", 0, 5));
        _animatedTileDatas.Push(new AnimatedTileData("Stars", 0, 5));
        _animatedTileDatas.Push(new AnimatedTileData("Windows", 0, 7, true));
        _animatedTileDatas.Push(new AnimatedTileData("BrokenWindows", 0, 7, true));
    }

    /// <summary>
    /// Fills the _sourceIdDict variable with its data
    /// </summary>
    private void _SetupSourceIdDictionary()
    {
        for (int i = 0; i < _tileMapLayer.TileSet.GetSourceCount(); i++)
        {
            TileSetSource source = _tileMapLayer.TileSet.GetSource(i);
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
        return _tileMapLayer.TileSet.GetSource(_GetSourceIdByName(name));
    }

    /// <summary>
    /// Gets all used active tiles, from a certain tilemap. relative to the given resource name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private Godot.Collections.Array<Vector2I> _GetUsedCellsByName(string name)
    {
        return _tileMapLayer.GetUsedCellsById(_GetSourceIdByName(name));
    }

    public override void _Process(double delta)
    {
        if (Timer)
        {
            foreach (var data in _animatedTileDatas)
            {
                foreach (var cell in _GetUsedCellsByName(data.RecourceName))
                {
                    Vector2I tile = _tileMapLayer.GetCellAtlasCoords(cell);
                    if (data.PingPong)
                    {
                        if (tile.X >= data.EndTile && data.AnimateForwards)
                            data.AnimateForwards = false;
                        if (tile.X <= data.StartTile && !data.AnimateForwards)
                            data.AnimateForwards = true;
                    }
                    else
                    {
                        if (tile.X >= data.EndTile && data.AnimateForwards)
                            tile.X = data.StartTile;
                    }

                    if (data.AnimateForwards)
                        _tileMapLayer.SetCell(cell, _GetSourceIdByName(data.RecourceName), new Vector2I(tile.X + 1, tile.Y));
                    else
                        _tileMapLayer.SetCell(cell, _GetSourceIdByName(data.RecourceName), new Vector2I(tile.X - 1, tile.Y));
                }
            }
            Timer = false;
        }
    }

    public void OnTimerTimeout()
    {
        Timer = true;
    }
}
