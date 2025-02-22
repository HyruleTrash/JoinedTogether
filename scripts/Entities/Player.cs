using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
    private Vector2I UP = new(0, -1);
    [Export]
    public int GRAVITY = 23;
    [Export]
    public int MAXFALLSPEED = 400;
    [Export]
    public int SPEED = 2;
    [Export]
    public int JUMPFORCE = 420;
    [Export]
    public int Inertia = 100;

    public Vector2 Motion = new();

    public override void _Ready()
    {
        base._Ready();
        SetUpDirection(UP);
        SetFloorStopOnSlopeEnabled(true);
        SetMaxSlides(4);
        SetFloorMaxAngle(MathF.PI / 4);
    }

    public override void _Process(double delta)
    {
        Motion.Y += GRAVITY;
        if (Motion.Y > MAXFALLSPEED)
            Motion.Y = MAXFALLSPEED;

        if (Input.IsActionPressed("RIGHT"))
        {
            Position = new(Position.X + SPEED, Position.Y);
        }
        else if (Input.IsActionPressed("LEFT"))
        {
            Position = new(Position.X - SPEED, Position.Y);
        }
        else
        {
            Motion.X = 0;
        }

        SetVelocity(Motion);
        MoveAndSlide();
        Motion.Y = Velocity.Y;

        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);
            if (IsOnFloor() && collision.GetNormal().Y < 1.0 && Motion.X != 0.0)
                Motion.Y = collision.GetNormal().Y;
        }
    }
}