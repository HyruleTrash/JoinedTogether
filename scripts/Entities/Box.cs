using Godot;
using System;
using System.Collections.Generic;

public partial class Box : RigidBody2D
{
    public Level Level;
    private Vector2I _spawnPosition = new();
    [Export]
    private PackedScene _deathParticleEffect;
    [Export]
    private AudioStreamPlayer2D _deathSound;
    private bool _freeze = false;

    public override void _Ready()
    {
        base._Ready();
        this._spawnPosition = (Vector2I)this.GlobalPosition;

        this.Level = GetNode<Level>("../../../");
        if (this.Level != null)
        {
            this.Level.NextLevelTriggered += UpdateState;
        }

        GlobalData globalData = GetNode<GlobalData>("/root/GlobalData");
        if (globalData != null)
        {
            globalData.ReloadLevel += () =>
            {
                if (IsInsideCurrentSubLevel())
                {
                    Respawn();
                }
            };
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        this.Freeze = this._freeze;
    }

    private bool IsInsideCurrentSubLevel()
    {
        return this.Level.CameraBoundingBoxManager.Camera.BoundingBox.IsPointWithinBounds(this._spawnPosition);
    }

    public void UpdateState()
    {
        if (IsInsideCurrentSubLevel())
        {
            this._freeze = false;
            this.Visible = true;
            Respawn();
        }
        else
        {
            this._freeze = true;
            this.Visible = false;
        }
    }

    public void Death()
    {
        this._deathSound.Play();
        Respawn();
    }

    public void Respawn()
    {
        this.Visible = false;

        // Death Particle
        GpuParticles2D instance = (GpuParticles2D)this._deathParticleEffect.Instantiate();
        instance.Position = this.GlobalPosition;
        GetNode("../").AddChild(instance);
        instance.Finished += () => { instance.QueueFree(); };

        // Respawn timer
        Timer t = new();
        t.SetWaitTime(0.1);
        t.SetOneShot(true);
        this.AddChild(t);
        t.Start();
        t.Timeout += () =>
        {
            this.LinearVelocity = new(0, 0);
            this.AngularVelocity = 0f;
            this.RotationDegrees = 0;
            this.GlobalPosition = this._spawnPosition;
            this.Visible = true;
            t.QueueFree();
        };
    }
}