using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// The player, the physical form the user uses inside the 2d game
/// </summary>
public partial class Player : CharacterBody2D
{
    // Misc, variables
    public PlayerState PlayerState;
    public PlayerMovement PlayerMovement;
    public PlayerRespawner PlayerRespawner;
    public PlayerAnimator PlayerAnimator;
    public PlayerSounds PlayerSounds;

    /// <summary>
    /// Causes player's death logic
    /// </summary>
    public void Die()
    {
        PlayerRespawner.Die();
    }

    /// <summary>
    /// Teleports the player to its respawn location
    /// </summary>
    public void Respawn()
    {
        PlayerRespawner.Respawn();
    }
}