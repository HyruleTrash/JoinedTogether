using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Adds all logic a box needs or uses, such as respawning, or when it should be active
/// </summary>
public partial class BoxRespawner : BoxComponent
{
    [Export]
    public double RESPAWN_TIME = 0.1;
    public Vector2I SpawnPosition = new();
    [Export]
    private PackedScene _deathParticleEffect;
    [Export]
    private AudioStreamPlayer2D _deathSound;

    public override void _Ready()
    {
        base._Ready();
        if (this._boxBody != null)
        {
            this._boxBody.BoxRespawner = this;
            this.SpawnPosition = (Vector2I)this._boxBody.GlobalPosition;

            // Make sure the box respawns when the level reload is triggered
            GlobalData globalData = GetNode<GlobalData>("/root/GlobalData");
            if (globalData != null)
            {
                globalData.ReloadLevel += () =>
                {
                    if (this._boxBody.BoxLevelTracker.IsInsideCurrentSubLevel())
                    {
                        Respawn();
                    }
                };
            }
        }
    }

    /// <summary>
    /// Causes box death logic
    /// </summary>
    public void Die()
    {
        this._deathSound.Play();

        // Death Particle
        GpuParticles2D instance = (GpuParticles2D)this._deathParticleEffect.Instantiate();
        instance.Position = this._boxBody.GlobalPosition;
        this._boxBody.GetNode("../").AddChild(instance);
        instance.Finished += () => { instance.QueueFree(); };

        Respawn();
    }

    /// <summary>
    /// Teleports the box to its respawn location, after a certain time period
    /// </summary>
    public void Respawn()
    {
        this._boxBody.Visible = false;

        // Respawn timer
        Timer t = new();
        t.SetWaitTime(RESPAWN_TIME);
        t.SetOneShot(true);
        this.AddChild(t);
        t.Start();
        t.Timeout += () =>
        {
            this._boxBody.LinearVelocity = new(0, 0);
            this._boxBody.AngularVelocity = 0f;
            this._boxBody.RotationDegrees = 0;
            this._boxBody.GlobalPosition = this.SpawnPosition;
            this._boxBody.Visible = true;
            t.QueueFree();
        };
    }
}