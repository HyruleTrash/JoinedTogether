using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Camera : Camera2D
{
    [Export]
    public Player Player;
    [Export]
    public Vector2 FollowOffset;
    [Export]
    public float Speed = 1;
    public Bounds2D BoundingBox = new();

    public override void _Process(double delta)
    {
        Vector2 newPosition = Position.Lerp(Player.Position - FollowOffset, (float)(delta * Speed));
        // LimitTop = (int)MathF.Round(BoundingBox.MinMaxY.X);
        LimitBottom = (int)MathF.Round(BoundingBox.MinMaxY.Y);
        LimitLeft = (int)MathF.Round(BoundingBox.MinMaxX.X);
        LimitRight = (int)MathF.Round(BoundingBox.MinMaxX.Y);
        Position = newPosition;
    }
}