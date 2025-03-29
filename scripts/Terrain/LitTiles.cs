using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Handles the logic for loading in all the lights for all the lit tiles
/// </summary>
public partial class LitTiles : Node2D
{
    private CustomTileMapLayer _tileMapLayer;
    [Export]
    public PackedScene LightPrefab;
    private static readonly Vector2I _TILESIZE = new(32, 32);
    private Stack<LitTileData> _litTileDatas = new Stack<LitTileData>();

    public override void _Ready()
    {
        base._Ready();
        SetupTileData();
        this._tileMapLayer = GetNode<CustomTileMapLayer>("../");
        this._tileMapLayer.SetupDone += SetupTileLightSources;
    }

    public void SetupTileData()
    {
        this._litTileDatas.Push(new LitTileData("Chains", new(0, 2), new(_TILESIZE.X * 0.5f, _TILESIZE.Y * 0.8f), new Color(0.8f, 1, 0.5f), 2.5f));
        this._litTileDatas.Push(new LitTileData("Windows", new(0, 2), _TILESIZE / 2, new Color(0.957f, 0.969f, 0.541f), 0.7f, true));
        this._litTileDatas.Push(new LitTileData("BrokenWindows", new(0, 2), _TILESIZE / 2, new Color(0.957f, 0.5f, 0.2f), 2.5f, true));
    }

    public void SetupTileLightSources()
    {
        foreach (LitTileData data in this._litTileDatas)
        {
            foreach (Vector2I cell in this._tileMapLayer.GetUsedCellsByName(data.RecourceName))
            {
                Vector2I tile = this._tileMapLayer.GetCellAtlasCoords(cell);
                if ((tile == data.Tile) || data.AllTilesOfResource)
                {
                    PointLight2D instance = (PointLight2D)this.LightPrefab.Instantiate();
                    instance.Position = (cell * _TILESIZE) + data.Offset;
                    instance.Energy = data.Energy;
                    instance.Color = data.Color;
                    instance.Name = "TileLightSource";
                    AddChild(instance);
                }
            }
        }
    }
}