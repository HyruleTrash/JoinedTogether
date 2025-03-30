using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// User input within the 2d world, to move the player character
/// </summary>
public partial class PlayerMovement : PlayerComponent
{
    // Const variables, sadly const c# doesnt really work with godot
    [Export]
    public float MAXFALLSPEED = 20000;
    [Export]
    public float SPEED = 220;
    [Export]
    public float JUMPFORCE = 934;

    // Physics variables
    [Export]
    public float Weight = 5;
    [Export]
    private float _jumpReleaseDeceleration = 0.5f;
    private float _gravity;
    private bool _coyoteTime;

    // Events
    [Signal]
    public delegate void OnPlayerJumpEventHandler();

    public override void _Ready()
    {
        base._Ready();

        this._playerBody.PlayerMovement = this;

        // Physics setup
        this._playerBody.SetUpDirection(this.UP);
        this._playerBody.SetFloorStopOnSlopeEnabled(true);
        this._playerBody.SetMaxSlides(4);
        this._playerBody.SetFloorMaxAngle(MathF.PI / 4);
        this._gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
    }

    protected override void _ProcessComponent(double delta)
    {
        _HorizontalMotion(delta);
        _VerticalMotion(delta);
        this._playerBody.MoveAndSlide();
    }

    /// <summary>
    /// The player's horizontal physics, and animation references
    /// </summary>
    private void _HorizontalMotion(double delta)
    {
        float direction = Input.GetAxis("LEFT", "RIGHT");
        if (direction != 0)
            this._playerBody.Velocity = new(this.SPEED * direction, this._playerBody.Velocity.Y);
        else
            this._playerBody.Velocity = new(0, this._playerBody.Velocity.Y);
    }

    /// <summary>
    /// The player's vertical physics
    /// </summary>
    private void _VerticalMotion(double delta)
    {
        // Gravity
        this._playerBody.Velocity = new(this._playerBody.Velocity.X, this._playerBody.Velocity.Y + this._gravity * this.Weight * (float)delta);

        // Limit negative vertical speed
        if (this._playerBody.Velocity.Y > this.MAXFALLSPEED)
            this._playerBody.Velocity = new(this._playerBody.Velocity.X, this.MAXFALLSPEED);

        // Jumping
        if (this._playerBody.IsOnFloor())
        {
            this._coyoteTime = true;
            if (Input.IsActionJustPressed("UP"))
            {
                _Jump(delta);
            }
        }
        else
        {
            _CoyoteTime();
            if (this._coyoteTime == true && Input.IsActionJustPressed("UP"))
            {
                _Jump(delta);
                this._coyoteTime = false;
            }
        }

        // Jump release deceleration
        if (Input.IsActionJustReleased("UP") && this._playerBody.Velocity.Y < 0)
            this._playerBody.Velocity = new(this._playerBody.Velocity.X, this._playerBody.Velocity.Y * this._jumpReleaseDeceleration);
    }

    /// <summary>
    /// Makes the player jump, when called in the physics function
    /// </summary>
    private void _Jump(double delta)
    {
        this._playerBody.Velocity = new(this._playerBody.Velocity.X, this.JUMPFORCE * this.UP.Y);
        EmitSignal(SignalName.OnPlayerJump);
    }

    /// <summary>
    /// Activates the coyote time timer
    /// </summary>
    private void _CoyoteTime()
    {
        Timer t = new();
        t.SetWaitTime(0.1);
        t.SetOneShot(true);
        this.AddChild(t);
        t.Start();
        t.Timeout += () =>
        {
            this._coyoteTime = false;
            t.QueueFree();
        };
    }
}