using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Causes player, and box entity respawn(death)
/// </summary>
public partial class DeathPlane : Area2D
{
    public override void _Ready()
    {
        base._Ready();
        this.BodyEntered += _OnBodyEntered;
    }

    private void _OnBodyEntered(object body)
    {
        if (body is Player player)
        {
            player.Death();
        }
        if (body is Box box)
        {
            box.Death();
        }
    }
}