using Godot;
using System;

public partial class MenuQuitItem : MenuItem, IMenuItemTriggerable
{
    public void TriggerItem()
    {
        GetTree().Quit();
    }
}
