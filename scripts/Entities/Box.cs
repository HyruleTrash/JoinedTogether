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
        _spawnPosition = (Vector2I)GlobalPosition;

        Level = GetNode<Level>("../../../");
        if (Level != null)
        {
            Level.NextLevelTriggered += UpdateState;
        }
        
        GlobalData globalData = GetNode<GlobalData>("/root/GlobalData");
        if (globalData != null)
        {
            globalData.ReloadLevel += () => {
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
        Freeze = _freeze;
    }

    private bool IsInsideCurrentSubLevel()
    {
        return Level.CameraBoundingBoxManager.Camera.BoundingBox.IsPointWithinBounds(_spawnPosition);
    }

    public void UpdateState()
    {
        if (IsInsideCurrentSubLevel())
        {
            _freeze = false;
            Visible = true;
            Respawn();
        }
        else
        {
            _freeze = true;
            Visible = false;
        }
    }

    public void Death()
    {
        _deathSound.Play();
        Respawn();
    }

    public void Respawn()
    {
        Visible = false;

        // Death Particle
        GpuParticles2D instance = (GpuParticles2D)_deathParticleEffect.Instantiate();
        instance.Position = GlobalPosition;
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
            LinearVelocity = new(0, 0);
            AngularVelocity = 0f;
            RotationDegrees = 0;
            GlobalPosition = _spawnPosition;
            Visible = true;
            t.QueueFree();
        };
    }
}