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
            return this.BoundingBox.MinMaxX;
        }
        set
        {
            this.BoundingBox.MinMaxX = value;
        }
    }
    [Export]
    public Vector2 MinMaxY
    {
        get
        {
            return this.BoundingBox.MinMaxY;
        }
        set
        {
            this.BoundingBox.MinMaxY = value;
        }
    }
    [Export]
    public Vector2 FollowOffset;

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (Engine.IsEditorHint())
        {
            DrawLine(
                new Vector2(this.BoundingBox.MinMaxX.X, this.BoundingBox.MinMaxY.X),
                new Vector2(this.BoundingBox.MinMaxX.X, this.BoundingBox.MinMaxY.Y),
                Colors.Red
                );
            DrawLine(
                new Vector2(this.BoundingBox.MinMaxX.Y, this.BoundingBox.MinMaxY.X),
                new Vector2(this.BoundingBox.MinMaxX.Y, this.BoundingBox.MinMaxY.Y),
                Colors.Red
                );
            DrawLine(
                new Vector2(this.BoundingBox.MinMaxX.X, this.BoundingBox.MinMaxY.X),
                new Vector2(this.BoundingBox.MinMaxX.Y, this.BoundingBox.MinMaxY.X),
                Colors.Red
                );
            DrawLine(
                new Vector2(this.BoundingBox.MinMaxX.X, this.BoundingBox.MinMaxY.Y),
                new Vector2(this.BoundingBox.MinMaxX.Y, this.BoundingBox.MinMaxY.Y),
                Colors.Red
                );
        }
    }

    public void UpdateDrawing()
    {
        QueueRedraw();
    }
}