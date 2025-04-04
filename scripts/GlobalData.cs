using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Holds all data, that's to be used globally throught the game logic
/// </summary>
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

    public Level GetLevel()
    {
        if (MainMenu == null || MainMenu.LevelOneInstance == null)
            return null;
        return MainMenu.LevelOneInstance;
    }

    public void SetEndScreen(bool state)
    {
        if (MainMenu == null)
            return;
        MainMenu.TempIsOnEndScreen = state;
    }

    public bool IsMenuVisible()
    {
        if (MainMenu == null)
            return false;
        return MainMenu.IsActive;
    }

    public bool IsPlayerInGirlState()
    {
        if (Player == null || Player.PlayerStateMachine == null)
            return false;
        return Player.PlayerStateMachine.IsInGirlState;
    }
}