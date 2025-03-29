using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the button logic, and its state
/// </summary>
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
        get => this._isPressed;
        set
        {
            this._isPressed = value;
            this.IsPressedStateChanged.Invoke();
        }
    }

    public override void _Ready()
    {
        base._Ready();
        this.AnimatedSprite2D.Play("default");
        this.BodyEntered += _OnBodyEntered;
        this.BodyExited += _OnBodyExited;
    }

    private void _OnBodyEntered(Node2D body)
    {
        UpdateOverlapLogic();
    }

    private void _OnBodyExited(Node2D body)
    {
        UpdateOverlapLogic();
    }

    /// <summary>
    /// Sets the button to the correct state depending on what is overlapping it
    /// </summary>
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
        if (legibleBodies.Count != 0 && !this.IsPressed)
        {
            this.IsPressed = true;
            this.ButtonOnSound.Play();
            this.AnimatedSprite2D.Play("pressed");
        }
        else if (legibleBodies.Count == 0 && this.IsPressed)
        {
            this.IsPressed = false;
            this.ButtonOffSound.Play();
            this.AnimatedSprite2D.Play("default");
        }
    }
}