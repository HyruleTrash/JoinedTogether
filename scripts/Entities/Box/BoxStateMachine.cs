using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Logic for when the box should be active or not. its active state
/// </summary>
public partial class BoxStateMachine : BoxComponent
{
    private bool _freeze = false;

    public override void _Ready()
    {
        base._Ready();
        if (this._boxBody != null && this._boxBody.BoxLevelTracker.Level != null)
        {
            this._boxBody.BoxStateMachine = this;
            this._boxBody.BoxLevelTracker.Level.NextLevelTriggered += UpdateState;
        }
    }

    protected override void _ProcessComponent(double delta)
    {
        this._boxBody.Freeze = this._freeze;
    }

    /// <summary>
    /// Update's the physics state of the box
    /// </summary>
    public void UpdateState()
    {
        if (this._boxBody.BoxLevelTracker.IsInsideCurrentSubLevel())
        {
            this._freeze = false;
            this._boxBody.Visible = true;
            this._boxBody.Respawn();
        }
        else
        {
            this._freeze = true;
            this._boxBody.Visible = false;
        }
    }
}