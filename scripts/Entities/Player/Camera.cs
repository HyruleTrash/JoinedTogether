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
        get => this._boundingBox;
        set
        {
            this._boundingBox = value;
            this.LimitTop = (int)MathF.Round(this._boundingBox.MinMaxY.X);
            this.LimitBottom = (int)MathF.Round(this._boundingBox.MinMaxY.Y);
            this.LimitLeft = (int)MathF.Round(this._boundingBox.MinMaxX.X);
            this.LimitRight = (int)MathF.Round(this._boundingBox.MinMaxX.Y);
        }
    }

    public override void _Process(double delta)
    {
        Vector2 newPosition = this.Position.Lerp(this.Player.Position - this.FollowOffset, (float)(delta * this.Speed));
        this.Position = newPosition;
    }
}