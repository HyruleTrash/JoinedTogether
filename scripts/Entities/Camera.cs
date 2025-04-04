using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A camera component, meant for following a 2d node around
/// </summary>
public partial class Camera : Camera2D
{
    [Export]
    public Node2D ToFollow;
    [Export]
    public Vector2 FollowOffset;
    [Export]
    public float Speed = 1;
    private Bounds2D _boundingBox = new();
    public Bounds2D BoundingBox
    {
        get => this._boundingBox;
        set
        {
            this._boundingBox = value;

            // Also sets the Camera2D bounding box logic
            this.LimitTop = (int)MathF.Round(this._boundingBox.MinMaxY.X);
            this.LimitBottom = (int)MathF.Round(this._boundingBox.MinMaxY.Y);
            this.LimitLeft = (int)MathF.Round(this._boundingBox.MinMaxX.X);
            this.LimitRight = (int)MathF.Round(this._boundingBox.MinMaxX.Y);
        }
    }

    public override void _Process(double delta)
    {
        // follow the player
        Vector2 newPosition = this.Position.Lerp(this.ToFollow.Position - this.FollowOffset, (float)(delta * this.Speed));
        this.Position = newPosition;
    }
}