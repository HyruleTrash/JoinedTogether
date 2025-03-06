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
    }
}