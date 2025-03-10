using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class LitTiles : Node2D
{
    private CustomTileMapLayer _tileMapLayer;
    [Export]
    public PackedScene LightPrefab;
    private static readonly Vector2I _TILESIZE = new(32, 32);

    public override void _Ready()
    {
        base._Ready();
        _tileMapLayer = GetNode<CustomTileMapLayer>("../");
        _tileMapLayer.SetupDone += () =>
        {
            foreach (Vector2I cell in _tileMapLayer.GetUsedCellsByName("Chains"))
            {
                Vector2I tile = _tileMapLayer.GetCellAtlasCoords(cell);
                Vector2I lightTile = new(0, 2);
                if (tile == lightTile)
                {
                    Node2D instance = (Node2D)LightPrefab.Instantiate();
                    instance.Position = (cell * _TILESIZE) + (_TILESIZE / 2);
                    AddChild(instance);
                }
            }
        };

    }
}