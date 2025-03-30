using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles a sublevel goal's visual state, and player level advancement logic
/// </summary>
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
    private bool _isActive = true;
    private bool _isPlayerAtDoor = false;
    private Player _player;

    public override void _Ready()
    {
        base._Ready();
        this.Area2D.BodyEntered += _OnBodyEntered;
        this.Area2D.BodyExited += _OnBodyExited;

        this.SubLevel = GetNode<SubLevel>("../");
        if (this.SubLevel != null)
        {
            this.SubLevel.IsCompletedStateChanged += UpdateState;
        }
        this.Level = GetNode<Level>("../../../");
    }

    /// <summary>
    /// Updates the door open and closed state
    /// </summary>
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
        _PlayerAdvanceCheck();
    }

    /// <summary>
    /// Checks if the player is allowed to advance
    /// </summary>
    private void _PlayerAdvanceCheck()
    {
        if (
            _isPlayerAtDoor &&
            this._isActive &&
            this.SubLevel.IsCompleted
        )
        {
            this._player.PlayerSounds.DoorSound.Play();
            this.Level.SetNextLevel();
            this._player.Respawn();
            this._isActive = false;
        }
    }

    private void _OnBodyEntered(object body)
    {
        if (
            body is Player player
        )
        {
            this._isPlayerAtDoor = true;
            this._player = player;
            _PlayerAdvanceCheck();
        }
    }

    private void _OnBodyExited(object body)
    {
        if (
            body is Player player
        )
        {
            this._isPlayerAtDoor = false;
            this._player = null;
        }
    }
}