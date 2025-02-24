using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalData : Node
{
    public Player Player;
    public MainMenu MainMenu;
    public int Level;
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