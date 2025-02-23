using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class GlobalData : Node2D
{
    public Player Player;
    public MainMenu MainMenu;
    public int Level;
    public int OldLevel = 1;

    // public override void _Process(double delta)
    // {

    // }
}