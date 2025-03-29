using Godot;
using System;
using System.Collections.Generic;

public partial class GoalArea : AnimatedSprite2D
{
    public SubLevel SubLevel;
    public Level Level;
    [Export]
    public Area2D Area2D;
    [Export]
    private AnimatedSprite2D _lightSprite;
    [Export]
    private PointLight2D _light;
    private static readonly Color _GREEN = new(0.2f, 1, 0);
    private static readonly Color _RED = new(1, 0, 0);
    private bool isActive = true;

    public override void _Ready()
    {
        base._Ready();
        this.Area2D.BodyEntered += _OnBodyEntered;

        this.SubLevel = GetNode<SubLevel>("../");
        if (this.SubLevel != null)
        {
            this.SubLevel.IsCompletedStateChanged += UpdateState;
        }
        this.Level = GetNode<Level>("../../../");
    }

    public void UpdateState()
    {
        if (this.SubLevel.IsCompleted)
        {
            Play("open");
            this._lightSprite.Play("green");
            this._light.Color = _GREEN;
        }
        else
        {
            Play("closed");
            this._lightSprite.Play("red");
            this._light.Color = _RED;
        }
    }

    private void _OnBodyEntered(object body)
    {
        if (
            body is Player player &&
            this.isActive &&
            this.SubLevel.IsCompleted
        )
        {
            player.DoorSound.Play();
            this.Level.SetNextLevel();
            player.Respawn();
            this.isActive = false;
        }
    }
}