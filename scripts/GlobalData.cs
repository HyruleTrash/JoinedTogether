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

    // Event used for reloading the level, and there for the current sublevel
    public Action ReloadLevel;
}