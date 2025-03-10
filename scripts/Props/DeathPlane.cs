using Godot;
using System;
using System.Collections.Generic;

public partial class DeathPlane : Area2D
{
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += _OnBodyEntered;
    }

    private void _OnBodyEntered(object body)
    {
        if (body is Player player)
        {
            player.Death();
        }
        if (body is Box box)
        {
            // TODO
            // #body.get_node("AudioStreamPlayer2D").stream = preload("res://Soundeffects/DeathSound.wav")
            // #body.ParticlesBox = preload("res://scenes/BoxParticles2D.tscn")
            // #body.respawn()
        }
    }
}