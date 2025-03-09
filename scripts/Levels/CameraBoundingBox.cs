using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

[Tool]
public partial class CameraBoundingBox : Node2D
{
    public Bounds2D BoundingBox = new();

    [Export]
    public Vector2 MinMaxX
    {
        get
        {
            return BoundingBox.MinMaxX;
        }
        set
        {
            BoundingBox.MinMaxX = value;
        }
    }
    [Export]
    public Vector2 MinMaxY
    {
        get
        {
            return BoundingBox.MinMaxY;
        }
        set
        {
            BoundingBox.MinMaxY = value;
        }
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        DrawLine(
            new Vector2(BoundingBox.MinMaxX.X, BoundingBox.MinMaxY.X),
            new Vector2(BoundingBox.MinMaxX.X, BoundingBox.MinMaxY.Y),
            Colors.Red
            );
        DrawLine(
            new Vector2(BoundingBox.MinMaxX.Y, BoundingBox.MinMaxY.X),
            new Vector2(BoundingBox.MinMaxX.Y, BoundingBox.MinMaxY.Y),
            Colors.Red
            );
        DrawLine(
            new Vector2(BoundingBox.MinMaxX.X, BoundingBox.MinMaxY.X),
            new Vector2(BoundingBox.MinMaxX.Y, BoundingBox.MinMaxY.X),
            Colors.Red
            );
        DrawLine(
            new Vector2(BoundingBox.MinMaxX.X, BoundingBox.MinMaxY.Y),
            new Vector2(BoundingBox.MinMaxX.Y, BoundingBox.MinMaxY.Y),
            Colors.Red
            );
    }

    public void UpdateDrawing()
    {
        QueueRedraw();
    }
}