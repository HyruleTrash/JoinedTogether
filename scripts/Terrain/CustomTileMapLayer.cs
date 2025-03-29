using Godot;
using System;
using System.Collections.Generic;

public partial class CustomTileMapLayer : TileMapLayer
{
    private Dictionary<string, int> _sourceIdDict = new Dictionary<string, int>();
    public event Action SetupDone;

    public override void _Ready()
    {
        base._Ready();
        _SetupSourceIdDictionary();
    }

    /// <summary>
    /// Fills the _sourceIdDict variable with its data
    /// </summary>
    private void _SetupSourceIdDictionary()
    {
        for (int i = 0; i < TileSet.GetSourceCount(); i++)
        {
            TileSetSource source = TileSet.GetSource(i);
            this._sourceIdDict[source.ResourceName] = i;
        }
        this.SetupDone.Invoke();
    }

    public Dictionary<string, int> GetSourceIdDict()
    {
        return new Dictionary<string, int>(this._sourceIdDict);
    }

    /// <summary>
    /// Gets the source id that is relative to the tilemap resource name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetSourceIdByName(string name)
    {
        if (this._sourceIdDict.TryGetValue(name, out int value))
            return value;
        else
            return -1;
    }

    /// <summary>
    /// Gets the source object, relative to the tilemap resource name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public TileSetSource GetSourceByName(string name)
    {
        return TileSet.GetSource(GetSourceIdByName(name));
    }

    /// <summary>
    /// Gets all used active tiles, from a certain tilemap. relative to the given resource name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Godot.Collections.Array<Vector2I> GetUsedCellsByName(string name)
    {
        int id = GetSourceIdByName(name);
        if (id == -1)
            return new Godot.Collections.Array<Vector2I>();
        return GetUsedCellsById(id);
    }
}