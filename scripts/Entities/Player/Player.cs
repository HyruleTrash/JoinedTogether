using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
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
        SetUpDirection(UP);
        SetFloorStopOnSlopeEnabled(true);
        SetMaxSlides(4);
        SetFloorMaxAngle(MathF.PI / 4);
        _gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

        // Misc setup
        GlobalData = GetNode<GlobalData>("/root/GlobalData");
        GlobalData.Player = this;
        GlobalData.ReloadLevel += Respawn;
        _animatedSprite2D.AnimationFinished += () => _StepSoundLogic();
        CorrectStates();
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
            DoorSound.Play();
            GlobalData.ReloadLevel?.Invoke();
        }
        if (Input.IsActionJustPressed("DOWN"))
        {
            SwitchStates();
            _changeSound.Play();
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
            if (IsOnFloor() && collision.GetNormal().Y < 1.0 && Velocity.X != 0.0)
                Velocity = new(Velocity.X, collision.GetNormal().Y);
            if (
                collision.GetCollider() is Box box && // Check if the collider is a box
                collision.GetNormal().Dot(UP) < 0.1f && // Check if the collision is not from the top
                playerPushAxis != 0 // Check if the player is pushing
            ){
                Vector2 pushDirection = Vector2.Right * playerPushAxis - new Vector2(0, -0.75f);
                box.ApplyCentralImpulse(pushDirection.Normalized() * PushForce);
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
            _animatedSprite2D.FlipH = direction < 0;
            _animatedSprite2D.Play(WalkState);
            Velocity = new(SPEED * direction, Velocity.Y);
        }
        else
        {
            _animatedSprite2D.Play(IdleState);
            Velocity = new(0, Velocity.Y);

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
    private void _VerticalMotion(double delta)
    {
        // Gravity
        Velocity = new(Velocity.X, Velocity.Y + _gravity * Weight * (float)delta);

        // Limit negative vertical speed
        if (Velocity.Y > MAXFALLSPEED)
            Velocity = new(Velocity.X, MAXFALLSPEED);
        
        // Jumping
        if (IsOnFloor())
        {
            _coyoteTime = true;
            if (Input.IsActionJustPressed("UP"))
            {
                _Jump(delta);
            }
        }
        else
        {
            _CoyoteTime();
            if (_coyoteTime)
            {
                if (Input.IsActionJustPressed("UP"))
                {
                    _Jump(delta);
                    _coyoteTime = false;
                }
            }

            // if falling
            if (Velocity.Y > 0)
            {
                _animatedSprite2D.Play(FallState);
            }
            else
            {
                _animatedSprite2D.Play(JumpState);
            }
        }

        // Jump release deceleration
        if (Input.IsActionJustReleased("UP") && Velocity.Y < 0)
        {
            Velocity = new(Velocity.X, Velocity.Y * _jumpReleaseDeceleration);
        }
    }

    /// <summary>
    /// Makes the player jump, when called in the physics function
    /// </summary>
    private void _Jump(double delta)
    {
        Velocity = new(Velocity.X, JUMPFORCE * UP.Y);
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
        IsInGirlState = !IsInGirlState;
        CorrectStates();
        EmitSignal(SignalName.OnStateSwitched);
    }

    /// <summary>
    /// Corrects the animation names, based on the set state
    /// </summary>
    public void CorrectStates()
    {
        if (!IsInGirlState)
        {
            JumpState = "JumpGirl";
            IdleState = "IdleGirl";
            FallState = "FallGirl";
            WalkState = "WalkGirl";
        }
        else
        {
            JumpState = "JumpBoy";
            IdleState = "IdleBoy";
            FallState = "FallBoy";
            WalkState = "WalkBoy";
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
            !_stepSound.Playing &&
            _animatedSprite2D.Frame == 1
        )
        {
            _stepSound.Play();
        }
    }

    public void Death()
    {
        _deathSound.Play();
        Respawn();
    }

    public void Respawn()
    {
        GlobalPosition = GlobalData.MainMenu.LevelOneInstance.CurrentSpawnPoint;
    }
}