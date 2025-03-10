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
        get => isCompleted;
        set
        {
            isCompleted = value;
            IsCompletedStateChanged.Invoke();
        }
    }
    [Export]
    public Array<Button> Buttons = new Array<Button>();

    public override void _Ready()
    {
        base._Ready();

        List<Button> list = new();
        foreach (Node item in GetChildren())
        {
            if (item is Button)
            {
                Button button = (Button)item;
                list.Add(button);
                button.IsPressedStateChanged += ButtonStateChanged;
            }
        }
        Buttons = new(list.ToArray());
        ButtonStateChanged();
    }

    public void ButtonStateChanged()
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
            IsCompleted = false;
        }
        else
        {
            IsCompleted = true;
        }
    }
}