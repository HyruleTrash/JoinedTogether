using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// The player, the physical form the user uses inside the 2d game
/// </summary>
public partial class TempOldPlayer : CharacterBody2D
{
    // Const variables, sadly const c# doesnt really work with godot
    private Vector2I UP = new(0, -1);
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
    public float PushForce = 20;
    [Export]
    private float _jumpReleaseDeceleration = 0.5f;
    private float _gravity;
    private bool _coyoteTime;

    // Animation variables
    [Export]
    private AnimatedSprite2D _animatedSprite2D;
    public string JumpState;
    public string IdleState;
    public string FallState;
    public string WalkState;

    // Sound variables
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

    // Misc, variables
    public GlobalData GlobalData;
    [Export]
    public bool IsInGirlState = false;
    [Signal]
    public delegate void OnStateSwitchedEventHandler();

    public override void _Ready()
    {
        base._Ready();

        // Physics setup
        SetUpDirection(this.UP);
        SetFloorStopOnSlopeEnabled(true);
        SetMaxSlides(4);
        SetFloorMaxAngle(MathF.PI / 4);
        this._gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

        // Misc setup
        this.GlobalData = GetNode<GlobalData>("/root/GlobalData");
        // this.GlobalData.Player = this;
        this.GlobalData.ReloadLevel += Respawn;
        this._animatedSprite2D.AnimationFinished += () => _StepSoundLogic();
        CorrectStates();
    }

    /// <summary>
    /// Returns if the player logic should be processing
    /// </summary>
    /// <returns>a boolean, that when true means that the process function should proceed with its logic</returns>
    private bool _ShouldProcess()
    {
        return (
            this._animatedSprite2D != null &&
            this._deathSound != null &&
            this._changeSound != null &&
            this._jumpSound != null &&
            this._stepSound != null
        );
    }

    public override void _Process(double delta)
    {
        if (!_ShouldProcess())
            return;

        if (Input.IsActionJustPressed("R")) // reload level
        {
            this.DoorSound.Play();
            this.GlobalData.ReloadLevel?.Invoke();
        }
        if (Input.IsActionJustPressed("DOWN")) // change level layout/state
        {
            SwitchStates();
            this._changeSound.Play();
        }

        _Physics(delta);
    }

    /// <summary>
    /// The physics function holds all logic regarding to player physics.
    /// Like player input(movement), and gravity
    /// </summary>
    private void _Physics(double delta)
    {
        _HorizontalMotion(delta);
        _VerticalMotion(delta);
        MoveAndSlide();

        // Collision logic
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            float playerPushAxis = Input.GetAxis("LEFT", "RIGHT");
            KinematicCollision2D collision = GetSlideCollision(i);
            if (
                collision.GetCollider() is Box box && // Check if the collider is a box
                collision.GetNormal().Dot(this.UP) < 0.1f && // Check if the collision is not from the top
                playerPushAxis != 0 &&// Check if the player is pushing
                box.Position.Y < this.GlobalPosition.Y // Check if the box is below the player
            )
            {
                Vector2 pushDirection = Vector2.Right * playerPushAxis - new Vector2(0, -0.75f);
                box.ApplyCentralImpulse(pushDirection.Normalized() * this.PushForce);
            }
        }
    }

    /// <summary>
    /// The player's horizontal physics, and animation references
    /// </summary>
    private void _HorizontalMotion(double delta)
    {
        float direction = Input.GetAxis("LEFT", "RIGHT");
        if (direction != 0)
        {
            this._animatedSprite2D.FlipH = direction < 0;
            this._animatedSprite2D.Play(this.WalkState);
            this.Velocity = new(this.SPEED * direction, this.Velocity.Y);
        }
        else
        {
            this._animatedSprite2D.Play(this.IdleState);
            this.Velocity = new(0, this.Velocity.Y);

        }

        if (IsOnFloor())
        {
            if (Input.IsActionJustPressed("LEFT") || Input.IsActionJustPressed("RIGHT"))
            {
                if (!this._stepSound.Playing)
                    this._stepSound.Play();
            }
        }
    }

    /// <summary>
    /// The player's vertical physics
    /// </summary>
    private void _VerticalMotion(double delta)
    {
        // Gravity
        this.Velocity = new(this.Velocity.X, this.Velocity.Y + this._gravity * this.Weight * (float)delta);

        // Limit negative vertical speed
        if (this.Velocity.Y > this.MAXFALLSPEED)
            this.Velocity = new(this.Velocity.X, this.MAXFALLSPEED);

        // Jumping
        if (IsOnFloor())
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
            if (this._coyoteTime)
            {
                if (Input.IsActionJustPressed("UP"))
                {
                    _Jump(delta);
                    this._coyoteTime = false;
                }
            }

            // if falling
            if (this.Velocity.Y > 0)
            {
                this._animatedSprite2D.Play(this.FallState);
            }
            else
            {
                this._animatedSprite2D.Play(this.JumpState);
            }
        }

        // Jump release deceleration
        if (Input.IsActionJustReleased("UP") && this.Velocity.Y < 0)
        {
            this.Velocity = new(this.Velocity.X, this.Velocity.Y * this._jumpReleaseDeceleration);
        }
    }

    /// <summary>
    /// Makes the player jump, when called in the physics function
    /// </summary>
    private void _Jump(double delta)
    {
        this.Velocity = new(this.Velocity.X, this.JUMPFORCE * this.UP.Y);
        this._jumpSound.Play();
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

    /// <summary>
    /// Switched the player state between boy and girl
    /// </summary>
    public void SwitchStates()
    {
        this.IsInGirlState = !this.IsInGirlState;
        CorrectStates();
        EmitSignal(SignalName.OnStateSwitched);
    }

    /// <summary>
    /// Corrects the animation names, based on the set state
    /// </summary>
    public void CorrectStates()
    {
        if (!this.IsInGirlState)
        {
            this.JumpState = "JumpGirl";
            this.IdleState = "IdleGirl";
            this.FallState = "FallGirl";
            this.WalkState = "WalkGirl";
        }
        else
        {
            this.JumpState = "JumpBoy";
            this.IdleState = "IdleBoy";
            this.FallState = "FallBoy";
            this.WalkState = "WalkBoy";
        }
    }

    /// <summary>
    /// Plays the step sound, incase the animated sprite is in the right state
    /// </summary>
    private void _StepSoundLogic()
    {
        if (
            IsOnFloor() &&
            (Input.IsActionJustPressed("LEFT") || Input.IsActionJustPressed("RIGHT")) &&
            !this._stepSound.Playing &&
            this._animatedSprite2D.Frame == 1
        )
        {
            this._stepSound.Play();
        }
    }

    /// <summary>
    /// Causes player's death logic
    /// </summary>
    public void Die()
    {
        this._deathSound.Play();
        this.GlobalData.ReloadLevel?.Invoke();
    }

    /// <summary>
    /// Teleports the player to its respawn location
    /// </summary>
    public void Respawn()
    {
        this.GlobalPosition = this.GlobalData.MainMenu.LevelOneInstance.CurrentSpawnPoint;
    }
}