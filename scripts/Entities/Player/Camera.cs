using Godot;
using System;
using System.Collections.Generic;

public partial class Camera : Node2D
{
    [Export]
    public Player Player;
    [Export]
    public float Speed = 1;
    public Bounds2D BoundingBox = new();

    public override void _Process(double delta)
    {
        Vector2 newPosition = Position.Lerp(Player.Position, (float)(delta * Speed));
        bool insideX;
        bool insideY;
        BoundingBox.IsPointWithinBounds(newPosition, out insideX, out insideY);
        if (insideX)
        {
            Position = new(newPosition.X, Position.Y);
        }
        if (insideY)
        {
            Position = new(Position.X, newPosition.Y);
        }
    }
}