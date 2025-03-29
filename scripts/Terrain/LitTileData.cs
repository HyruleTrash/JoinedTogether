using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Holds all data for adding a light to a tile
/// </summary>
class LitTileData
{
    public string RecourceName = "";
    public Vector2I Tile = new(0, 0);
    public float Energy = 1;
    public Vector2 Offset = new(0, 0);
    public Color Color = new(1, 1, 1, 1);
    public bool AllTilesOfResource = false;

    public LitTileData(string RecourceName, Vector2I Tile, Vector2 Offset, Color Color, float Energy = 1, bool AllTilesOfResource = false)
    {
        this.RecourceName = RecourceName;
        this.Tile = Tile;
        this.Offset = Offset;
        this.Color = Color;
        this.Energy = Energy;
        this.AllTilesOfResource = AllTilesOfResource;
    }
}