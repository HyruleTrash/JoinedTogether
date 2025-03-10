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
        Area2D.BodyEntered += _OnBodyEntered;

        SubLevel = GetNode<SubLevel>("../");
        if (SubLevel != null)
        {
            SubLevel.IsCompletedStateChanged += UpdateState;
        }
        Level = GetNode<Level>("../../../");
    }

    public void UpdateState()
    {
        if (SubLevel.IsCompleted)
        {
            Play("open");
            _lightSprite.Play("green");
            _light.Color = _GREEN;
        }
        else
        {
            Play("closed");
            _lightSprite.Play("red");
            _light.Color = _RED;
        }
    }

    private void _OnBodyEntered(object body)
    {
        if (
            body is Player player &&
            isActive &&
            SubLevel.IsCompleted
        )
        {
            player.DoorSound.Play();
            Level.SetNextLevel();
            player.Respawn();
            isActive = false;
        }
    }
}