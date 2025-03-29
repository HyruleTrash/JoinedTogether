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
        myTimer.WaitTime = this.WaitTime;
        myTimer.Autostart = true;
        myTimer.Timeout += () => OnTimerTimeout();
        AddChild(myTimer);

        this._tileMapLayer = GetNode<CustomTileMapLayer>("../");

        _SetupAnimatedTiles();
    }

    /// <summary>
    /// Fills the collection _animatedTileDatas, with the data needed to animate different tiles from tilemaps
    /// </summary>
    private void _SetupAnimatedTiles()
    {
        this._animatedTileDatas.Push(new AnimatedTileData("RedGradient", 0, 4, true));
        this._animatedTileDatas.Push(new AnimatedTileData("Moons", 0, 5));
        this._animatedTileDatas.Push(new AnimatedTileData("Stars", 0, 5));
        this._animatedTileDatas.Push(new AnimatedTileData("Windows", 0, 7, true));
        this._animatedTileDatas.Push(new AnimatedTileData("BrokenWindows", 0, 7, true));
    }

    public override void _Process(double delta)
    {
        if (this.Timer)
        {
            foreach (var data in this._animatedTileDatas)
            {
                foreach (var cell in this._tileMapLayer.GetUsedCellsByName(data.RecourceName))
                {
                    Vector2I tile = this._tileMapLayer.GetCellAtlasCoords(cell);
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
                        this._tileMapLayer.SetCell(cell, this._tileMapLayer.GetSourceIdByName(data.RecourceName), new Vector2I(tile.X + 1, tile.Y));
                    else
                        this._tileMapLayer.SetCell(cell, this._tileMapLayer.GetSourceIdByName(data.RecourceName), new Vector2I(tile.X - 1, tile.Y));
                }
            }
            this.Timer = false;
        }
    }

    public void OnTimerTimeout()
    {
        this.Timer = true;
    }
}
