using Godot;
using System;

public partial class MenuQuitItem : MenuItem
{
    public override void TriggerItem()
    {
        GetTree().Quit();
    }
}
