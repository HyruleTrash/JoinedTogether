using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles a entity respawn logic
/// </summary>
public partial class EntityRespawner : Node
{
    [Signal]
    public delegate void OnEntityDeathEventHandler();
    protected Node2D _body;
    protected Vector2 _respawnPoint;

    public override void _Ready()
    {
        base._Ready();
        if (GetParent() is Node2D parent)
            _body = parent;
    }

    /// <summary>
    /// Causes player's death logic
    /// </summary>
    public virtual void Die()
    {
        EmitSignal(SignalName.OnEntityDeath);
        Respawn();
    }

    /// <summary>
    /// Teleports the player to its respawn location
    /// </summary>
    public virtual void Respawn()
    {
        this._body.GlobalPosition = this._respawnPoint;
    }
}