using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the playing of soundsm made by the player character
/// </summary>
public partial class PlayerSounds : PlayerComponent
{
    [Export]
    private AudioStreamPlayer2D _deathSound;
    [Export]
    private AudioStreamPlayer2D _changeSound;
    [Export]
    private AudioStreamPlayer2D _jumpSound;
    [Export]
    private AudioStreamPlayer2D _stepSound;
    [Export]
    public AudioStreamPlayer2D DoorSound;

    public override void _Ready()
    {
        base._Ready();

        this._playerBody.PlayerSounds = this;

        this._playerBody.PlayerState.OnStateSwitched += () => this._changeSound.Play();
        this._playerBody.PlayerMovement.OnPlayerJump += () => this._jumpSound.Play();
        this._playerBody.PlayerRespawner.OnPlayerLevelReloadTrigger += () => this.DoorSound.Play();
        this._playerBody.PlayerRespawner.OnPlayerDeath += () => this._deathSound.Play();
        this._playerBody.PlayerAnimator.AnimatedSprite2D.AnimationFinished += () =>
        {
            if (this._playerBody.PlayerAnimator.AnimatedSprite2D.Frame == 1)
            {
                _StepSoundLogic();
            }
        };
    }

    protected override void _ProcessComponent(double delta)
    {
        _StepSoundLogic();
    }

    /// <summary>
    /// Plays the step sound, incase the player is moving and its feet hit the ground
    /// </summary>
    private void _StepSoundLogic()
    {
        if (
            this._playerBody.IsOnFloor() &&
            (Input.IsActionJustPressed("LEFT") || Input.IsActionJustPressed("RIGHT")) &&
            !this._stepSound.Playing
        )
        {
            this._stepSound.Play();
        }
    }
}