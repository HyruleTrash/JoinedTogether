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

        Level level = GetNode<Level>("../");
        if (level != null)
            level.SpawnPointManager = this;
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
}