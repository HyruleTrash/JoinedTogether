using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the player's states, and their switching logic
/// </summary>
public partial class PlayerState : PlayerComponent
{
    [Export]
    public bool IsInGirlState = false;
    public string JumpState;
    public string IdleState;
    public string FallState;
    public string WalkState;
    [Signal]
    public delegate void OnStateSwitchedEventHandler();

    public override void _Ready()
    {
        base._Ready();
        this._playerBody.PlayerState = this;
        CorrectStates();
    }

    protected override void _ProcessComponent(double delta)
    {
        if (Input.IsActionJustPressed("DOWN")) // change level layout/state
        {
            SwitchStates();
        }
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
}