using Godot;
using System;

/// <summary>
/// Ends the game
/// </summary>
public partial class MenuQuitItem : MenuItem, IMenuItemTriggerable
{
    public void TriggerItem()
    {
        GetTree().Quit();
    }
}
