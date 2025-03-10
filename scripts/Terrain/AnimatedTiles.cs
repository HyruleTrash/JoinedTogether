using Godot;
using System;
using System.Collections.Generic;

public partial class AnimatedTiles : Node
{
    [Export]
    public float WaitTime = 0.2f;
    [Export]
    public bool Timer = true;
    private CustomTileMapLayer _tileMapLayer;
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

        _tileMapLayer = GetNode<CustomTileMapLayer>("../");

        _SetupAnimatedTiles();
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

    public override void _Process(double delta)
    {
        if (Timer)
        {
            foreach (var data in _animatedTileDatas)
            {
                foreach (var cell in _tileMapLayer.GetUsedCellsByName(data.RecourceName))
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
                        _tileMapLayer.SetCell(cell, _tileMapLayer.GetSourceIdByName(data.RecourceName), new Vector2I(tile.X + 1, tile.Y));
                    else
                        _tileMapLayer.SetCell(cell, _tileMapLayer.GetSourceIdByName(data.RecourceName), new Vector2I(tile.X - 1, tile.Y));
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
