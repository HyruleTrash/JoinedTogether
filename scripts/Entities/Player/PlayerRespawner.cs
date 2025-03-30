using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the player's respawn logic
/// </summary>
public partial class PlayerRespawner : PlayerComponent
{
    [Signal]
    public delegate void OnPlayerLevelReloadTriggerEventHandler();
    [Signal]
    public delegate void OnPlayerDeathEventHandler();

    public override void _Ready()
    {
        base._Ready();
        this._playerBody.PlayerRespawner = this;
        this.GlobalData.ReloadLevel += Respawn;
    }

    protected override void _ProcessComponent(double delta)
    {
        if (Input.IsActionJustPressed("R")) // reload level
        {
            this.GlobalData.ReloadLevel?.Invoke();
            EmitSignal(SignalName.OnPlayerLevelReloadTrigger);
        }
    }

    /// <summary>
    /// Causes player's death logic
    /// </summary>
    public void Die()
    {
        EmitSignal(SignalName.OnPlayerDeath);
        this.GlobalData.ReloadLevel?.Invoke();
    }

    /// <summary>
    /// Teleports the player to its respawn location
    /// </summary>
    public void Respawn()
    {
        this._playerBody.GlobalPosition = this.GlobalData.MainMenu.LevelOneInstance.CurrentSpawnPoint;
    }
}