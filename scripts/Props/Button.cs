using Godot;
using System;
using System.Collections.Generic;

public partial class Button : Area2D
{
    [Export]
    private AnimatedSprite2D AnimatedSprite2D;
    [Export]
    private AudioStreamPlayer2D ButtonOnSound;
    [Export]
    private AudioStreamPlayer2D ButtonOffSound;
    public event Action IsPressedStateChanged;
    private bool _isPressed = false;
    public bool IsPressed
    {
        get => _isPressed;
        set
        {
            _isPressed = value;
            IsPressedStateChanged.Invoke();
        }
    }

    public override void _Ready()
    {
        base._Ready();
        AnimatedSprite2D.Play("default");
        BodyEntered += _OnBodyEntered;
        BodyExited += _OnBodyExited;
    }

    private void _OnBodyEntered(Node2D body)
    {
        UpdateOverlapLogic();
    }

    private void _OnBodyExited(Node2D body)
    {
        UpdateOverlapLogic();
    }

    private void UpdateOverlapLogic()
    {
        Godot.Collections.Array<Node2D> bodies = GetOverlappingBodies();
        List<Node2D> legibleBodies = new();
        foreach (Node2D item in bodies)
        {
            if (item is Player || item is Box)
            {
                legibleBodies.Add(item);
            }
        }
        if (legibleBodies.Count != 0 && !IsPressed)
        {
            IsPressed = true;
            ButtonOnSound.Play();
            AnimatedSprite2D.Play("pressed");
        }
        else if (legibleBodies.Count == 0 && IsPressed)
        {
            IsPressed = false;
            ButtonOffSound.Play();
            AnimatedSprite2D.Play("default");
        }
    }
}