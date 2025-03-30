using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Adds all logic a box needs or uses, such as respawning, or when it should be active
/// </summary>
public partial class Box : RigidBody2D
{
    public BoxRespawner BoxRespawner;
    public BoxStateMachine BoxStateMachine;
    public BoxLevelTracker BoxLevelTracker;

    /// <summary>
    /// Causes box death logic
    /// </summary>
    public void Die()
    {
        BoxRespawner.Die();
    }

    /// <summary>
    /// Teleports the box to its respawn location, after a certain time period
    /// </summary>
    public void Respawn()
    {
        BoxRespawner.Respawn();
    }
}