using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the player's states, and their switching logic
/// </summary>
public partial class PlayerStateMachine : PlayerComponent
{
    [Export]
    public bool IsInGirlState = false;
    public PlayerState JumpState = new("JumpBoy", "JumpGirl");
    public PlayerState IdleState = new("IdleBoy", "IdleGirl");
    public PlayerState FallState = new("FallBoy", "FallGirl");
    public PlayerState WalkState = new("WalkBoy", "WalkGirl");
    [Signal]
    public delegate void OnStateSwitchedEventHandler();

    public override void _Ready()
    {
        base._Ready();
        this._playerBody.PlayerStateMachine = this;
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
        this.JumpState.SetState(this.IsInGirlState);
        this.IdleState.SetState(this.IsInGirlState);
        this.FallState.SetState(this.IsInGirlState);
        this.WalkState.SetState(this.IsInGirlState);
    }
}