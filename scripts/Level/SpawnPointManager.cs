using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Holds a list of all the spawnpoints, per sublevel
/// </summary>
public partial class SpawnPointManager : Node
{
    [Export]
    private Array<Node2D> _spawnPoints = new Array<Node2D>(); // this array is used and accesed using the sub level index
    public Vector2 CurrentSpawnPoint;

    public override void _Ready()
    {
        base._Ready();

        LoadSpawnPoints();
        Level level = GetNode<Level>("../");
        if (level != null)
        {
            level.SpawnPointManager = this;
            level.UpdateSpawnPoint();
        }
    }

    /// <summary>
    /// Returns the spawnpoint relative to the given sublevel
    /// </summary>
    /// <param name="index">sublevel index</param>
    /// <param name="isInIndexRange">Since a Vector2.Zero can be a actual location, use this bool to check if the spawnpoint exists</param>
    /// <returns>The spawn location</returns>
    public Vector2 GetSpawnPoint(int index, out bool isInIndexRange)
    {
        if (index < 0 || index >= this._spawnPoints.Count)
        {
            isInIndexRange = false;
            return Vector2.Zero;
        }

        isInIndexRange = true;
        return this._spawnPoints[index].Position;
    }

    /// <summary>
    /// Loads all spawnpoints into the array
    /// </summary>
    public void LoadSpawnPoints()
    {
        List<Node2D> list = new();
        foreach (Node item in GetChildren())
        {
            if (item is Node2D)
            {
                list.Add((Node2D)item);
            }
        }
        this._spawnPoints = new(list.ToArray());
    }
}