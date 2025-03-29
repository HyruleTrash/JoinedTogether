using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class SubLevel : Node
{
    public event Action IsCompletedStateChanged;
    private bool isCompleted = false;
    [Export]
    public bool IsCompleted
    {
        get => this.isCompleted;
        set
        {
            this.isCompleted = value;
            this.IsCompletedStateChanged.Invoke();
        }
    }
    [Export]
    public Array<Button> Buttons = new Array<Button>();

    public override void _Ready()
    {
        base._Ready();

        // Loads all buttons found in the current sublevel into an array, for exit/win condition
        List<Button> list = new();
        foreach (Node item in GetChildren())
        {
            if (item is Button)
            {
                Button button = (Button)item;
                list.Add(button);
                button.IsPressedStateChanged += CheckExitCondition;
            }
        }
        Buttons = new(list.ToArray());
        CheckExitCondition();
    }

    /// <summary>
    /// Triggers the logic for checking if the win/exit sublevel condition is reached
    /// </summary>
    public void CheckExitCondition()
    {
        bool WasUnpressedButtonFound = false;
        foreach (Button button in Buttons)
        {
            if (button.IsPressed == false)
            {
                WasUnpressedButtonFound = true;
                break;
            }
        }
        if (WasUnpressedButtonFound)
        {
            this.IsCompleted = false;
        }
        else
        {
            this.IsCompleted = true;
        }
    }
}