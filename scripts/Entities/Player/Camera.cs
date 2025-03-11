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
    private Bounds2D _boundingBox = new();
    public Bounds2D BoundingBox
    {
        get => _boundingBox;
        set
        {
            _boundingBox = value;
            LimitTop = (int)MathF.Round(_boundingBox.MinMaxY.X);
            LimitBottom = (int)MathF.Round(_boundingBox.MinMaxY.Y);
            LimitLeft = (int)MathF.Round(_boundingBox.MinMaxX.X);
            LimitRight = (int)MathF.Round(_boundingBox.MinMaxX.Y);
        }
    }

    public override void _Process(double delta)
    {
        Vector2 newPosition = Position.Lerp(Player.Position - FollowOffset, (float)(delta * Speed));
        Position = newPosition;
    }
}