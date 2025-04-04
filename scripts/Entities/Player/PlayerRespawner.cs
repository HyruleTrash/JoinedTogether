using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the player's respawn logic
/// </summary>
public partial class PlayerRespawner : EntityRespawner
{
    [Signal]
    public delegate void OnPlayerLevelReloadTriggerEventHandler();
    public GlobalData GlobalData;

    public override void _Ready()
    {
        base._Ready();

        Player playerBody;
        if (GetParent() is Player parent)
        {
            playerBody = parent;
            playerBody.PlayerRespawner = this;
        }

        this.GlobalData = GetNode<GlobalData>("/root/GlobalData");
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("R")) // reload level
        {
            this.GlobalData.ReloadLevel?.Invoke();
            EmitSignal(SignalName.OnPlayerLevelReloadTrigger);
            Respawn();
        }
    }

    /// <summary>
    /// Causes player's death logic
    /// </summary>
    public override void Die()
    {
        base.Die();
        this.GlobalData.ReloadLevel?.Invoke();
    }

    /// <summary>
    /// Teleports the player to its respawn location
    /// </summary>
    public override void Respawn()
    {
        this._respawnPoint = this.GlobalData.GetLevel().GetCurrentSpawnPoint();
        base.Respawn();
    }
}