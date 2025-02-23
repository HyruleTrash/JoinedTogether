using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
    // Const variables, sadly const c# doesnt really work with godot
    private Vector2I UP = new(0, -1);
    [Export]
    public int GRAVITY = 23;
    [Export]
    public int MAXFALLSPEED = 400;
    [Export]
    public int SPEED = 2;
    [Export]
    public int JUMPFORCE = 420;
    // [Export]
    // public int Inertia = 100;

    // Physics variables
    public Vector2 Motion = new();
    private bool _coyoteTime;

    // Animation variables
    [Export]
    private AnimatedSprite2D _animatedSprite2D;
    public string JumpState = "JumpBoy";
    public string IdleState = "IdleBoy";
    public string FallState = "FallBoy";
    public string WalkState = "WalkBoy";

    // Sound variables
    [Export]
    private AudioStreamPlayer2D _deathSound;
    [Export]
    private AudioStreamPlayer2D _changeSound;
    [Export]
    private AudioStreamPlayer2D _jumpSound;
    [Export]
    private AudioStreamPlayer2D _stepSound;

    // Misc, variables
    [Export]
    public bool IsInGirlState = false;

    public override void _Ready()
    {
        base._Ready();

        // Physics setup
        SetUpDirection(UP);
        SetFloorStopOnSlopeEnabled(true);
        SetMaxSlides(4);
        SetFloorMaxAngle(MathF.PI / 4);

        // Misc setup
        GetNode<GlobalData>("/root/GlobalData").Player = this;
        _animatedSprite2D.AnimationFinished += () => _StepSoundLogic();
    }

    private bool _ShouldRun()
    {
        return (
            _animatedSprite2D != null &&
            _deathSound != null &&
            _changeSound != null &&
            _jumpSound != null &&
            _stepSound != null
        );
    }

    public override void _Process(double delta)
    {
        if (!_ShouldRun())
            return;

        if (Input.IsActionJustPressed("R"))
        {
            // $DeathSound.stream = preload("res://Soundeffects/Door.wav")
            // _Respawn();
        }
        if (Input.IsActionJustPressed("DOWN"))
        {
            SwitchStates();
            _changeSound.Play();
        }

        _Physics();
    }

    /// <summary>
    /// The physics function holds all logic regarding to player physics.
    /// Like player input(movement), and gravity
    /// </summary>
    private void _Physics()
    {
        _HorizontalMotion();
        _VerticalMotion();
        SetVelocity(Motion);
        MoveAndSlide();

        // Collision logic
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);
            if (IsOnFloor() && collision.GetNormal().Y < 1.0 && Motion.X != 0.0)
                Motion.Y = collision.GetNormal().Y;
            // if collision.collider.is_in_group("box"):
            //     var box = collision.collider
            //     box.apply_central_impulse(- collision.normal * inertia)
            //     if global_position.y == box.global_position.y:
            //         if global_position.x > box.global_position.x:
            //             box.rotation_degrees -= 1
            //         if global_position.x < box.global_position.x:
            //             box.rotation_degrees += 1
        }
    }

    /// <summary>
    /// The player's horizontal physics, and animation references
    /// </summary>
    private void _HorizontalMotion()
    {
        if (Input.IsActionPressed("RIGHT"))
        {
            Position = new(Position.X + SPEED, Position.Y);
            _animatedSprite2D.Play(WalkState);
            _animatedSprite2D.FlipH = false;
        }
        else if (Input.IsActionPressed("LEFT"))
        {
            Position = new(Position.X - SPEED, Position.Y);
            _animatedSprite2D.Play(WalkState);
            _animatedSprite2D.FlipH = true;
        }
        else
        {
            _animatedSprite2D.Play(IdleState);
            Motion.X = 0;
        }

        if (IsOnFloor())
        {
            if (Input.IsActionJustPressed("LEFT") || Input.IsActionJustPressed("RIGHT"))
            {
                if (!_stepSound.Playing)
                    _stepSound.Play();
            }
        }
    }

    /// <summary>
    /// The player's vertical physics
    /// </summary>
    private void _VerticalMotion()
    {
        Motion.Y = Velocity.Y;
        // Gravity
        Motion.Y += GRAVITY;
        // Limit negative vertical speed
        if (Motion.Y > MAXFALLSPEED)
            Motion.Y = MAXFALLSPEED;
        if (IsOnFloor())
        {
            _coyoteTime = true;
            if (Input.IsActionJustPressed("UP"))
            {
                _Jump();
            }
        }
        else
        {
            _CoyoteTime();
            if (_coyoteTime)
            {
                if (Input.IsActionJustPressed("UP"))
                {
                    _Jump();
                    _coyoteTime = false;
                }
            }

            // if falling
            if (Motion.Y > 0)
            {
                _animatedSprite2D.Play(FallState);
            }
            else
            {
                _animatedSprite2D.Play(JumpState);
            }
        }
    }

    /// <summary>
    /// Makes the player jump, when called in the physics function
    /// </summary>
    private void _Jump()
    {
        Motion.Y = -JUMPFORCE;
        _jumpSound.Play();
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
            _coyoteTime = false;
            t.QueueFree();
        };
    }

    /// <summary>
    /// Switched the player state between boy and girl
    /// </summary>
    public void SwitchStates()
    {
        if (!IsInGirlState)
        {
            JumpState = "JumpGirl";
            IdleState = "IdleGirl";
            FallState = "FallGirl";
            WalkState = "WalkGirl";
        }
        else if (IsInGirlState)
        {
            JumpState = "JumpBoy";
            IdleState = "IdleBoy";
            FallState = "FallBoy";
            WalkState = "WalkBoy";
        }
        IsInGirlState = !IsInGirlState;
    }

    /// <summary>
    /// Plays the step sound, incase the animated sprite is in the right state
    /// </summary>
    private void _StepSoundLogic()
    {
        if (
            IsOnFloor() &&
            (Input.IsActionJustPressed("LEFT") || Input.IsActionJustPressed("RIGHT")) &&
            !_stepSound.Playing &&
            _animatedSprite2D.Frame == 1
        )
        {
            _stepSound.Play();
        }
    }
}