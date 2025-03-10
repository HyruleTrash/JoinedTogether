using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class SpawnPointManager : Node
{
    [Export]
    private Array<Node2D> _spawnPoints = new Array<Node2D>();

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

    public Vector2 GetSpawnPoint(int index, out bool isInIndexRange)
    {
        if (index < 0 || index >= _spawnPoints.Count)
        {
            isInIndexRange = false;
            return Vector2.Zero;
        }

        isInIndexRange = true;
        return _spawnPoints[index].Position;
    }

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
        _spawnPoints = new(list.ToArray());
    }
}