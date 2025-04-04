using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// A base class for obtaining player's physics and events
/// </summary>
public partial class PlayerComponent : Node
{
    protected static readonly Vector2I _UP = new(0, -1);
    protected Player _playerBody;
    public GlobalData GlobalData;

    public override void _Ready()
    {
        base._Ready();

        if (GetParent() is Player parent)
        {
            this._playerBody = parent;
            this.GlobalData = GetNode<GlobalData>("/root/GlobalData");
            this.GlobalData.Player = this._playerBody;
        }
        else
        {
            GD.PushError("PlayerMovement component is not a child of a Player");
            return;
        }
    }

    /// <summary>
    /// Returns if the player logic should be processing
    /// </summary>
    /// <returns>a boolean, that when true means that the process function should proceed with its logic</returns>
    protected virtual bool _ShouldProcess()
    {
        return (
            this._playerBody != null
        );
    }

    public override void _Process(double delta)
    {
        if (!_ShouldProcess())
            return;

        _ProcessComponent(delta);
    }

    /// <summary>
    /// A custom process function only triggered when the _ShouldProcess condition is met
    /// </summary>
    protected virtual void _ProcessComponent(double delta)
    {
        // Prefered virtual non implementation over, interface
    }
}