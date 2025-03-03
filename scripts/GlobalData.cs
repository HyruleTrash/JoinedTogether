using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalData : Node
{
    [Export]
    public Player Player;
    [Export]
    public MainMenu MainMenu;
    [Export]
    public int Level;
    [Export]
    public int OldLevel = 1;

    public override void _Ready()
    {
        base._Ready();
        // GD.Print("GlobalData Ready");
    }

    // public override void _Process(double delta)
    // {

    // }
}