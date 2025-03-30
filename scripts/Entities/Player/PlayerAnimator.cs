using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the player's animations
/// </summary>
public partial class PlayerAnimator : PlayerComponent
{
    // Animation variables
    [Export]
    public AnimatedSprite2D AnimatedSprite2D;

    public override void _Ready()
    {
        base._Ready();

        this._playerBody.PlayerAnimator = this;
    }

    protected override void _ProcessComponent(double delta)
    {
        float direction = Input.GetAxis("LEFT", "RIGHT");
        if (direction != 0)
        {
            this.AnimatedSprite2D.FlipH = direction < 0;
            this.AnimatedSprite2D.Play(this._playerBody.PlayerStateMachine.WalkState);
        }
        else
        {
            this.AnimatedSprite2D.Play(this._playerBody.PlayerStateMachine.IdleState);
        }

        // Jumping
        if (!this._playerBody.IsOnFloor())
        {
            // if falling
            if (this._playerBody.Velocity.Y > 0)
            {
                this.AnimatedSprite2D.Play(this._playerBody.PlayerStateMachine.FallState);
            }
            else
            {
                this.AnimatedSprite2D.Play(this._playerBody.PlayerStateMachine.JumpState);
            }
        }
    }
}