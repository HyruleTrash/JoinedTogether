using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Adds all logic a box needs or uses, such as respawning, or when it should be active
/// </summary>
public partial class BoxRespawner : EntityRespawner
{
    [Export]
    public double RESPAWN_TIME = 0.1;
    [Export]
    private PackedScene _deathParticleEffect;
    [Export]
    private AudioStreamPlayer2D _deathSound;

    public override void _Ready()
    {
        base._Ready();
        if (GetParent() is Box box)
        {
            box.BoxRespawner = this;
            this._respawnPoint = (Vector2I)box.GlobalPosition;

            // Make sure the box respawns when the level reload is triggered
            GlobalData globalData = GetNode<GlobalData>("/root/GlobalData");
            if (globalData != null)
            {
                globalData.ReloadLevel += () =>
                {
                    if (box.BoxLevelTracker.IsInsideCurrentSubLevel())
                    {
                        Respawn();
                    }
                };
            }
        }
    }

    public Vector2 GetSpawnPoint()
    {
        return this._respawnPoint;
    }

    /// <summary>
    /// Causes box death logic
    /// </summary>
    public override void Die()
    {
        this._deathSound.Play();

        // Death Particle
        GpuParticles2D instance = (GpuParticles2D)this._deathParticleEffect.Instantiate();
        instance.Position = this._body.GlobalPosition;
        this._body.GetNode("../").AddChild(instance);
        instance.Finished += () => { instance.QueueFree(); };

        base.Die();
    }

    /// <summary>
    /// Teleports the box to its respawn location, after a certain time period
    /// </summary>
    public override void Respawn()
    {
        this._body.Visible = false;

        // Respawn timer
        if (this._body is Box box)
        {
            Timer t = new();
            t.SetWaitTime(RESPAWN_TIME);
            t.SetOneShot(true);
            this.AddChild(t);
            t.Start();
            t.Timeout += () =>
            {
                box.LinearVelocity = new(0, 0);
                box.AngularVelocity = 0f;
                box.RotationDegrees = 0;
                box.Visible = true;
                base.Respawn();
                t.QueueFree();
            };
        }
    }
}