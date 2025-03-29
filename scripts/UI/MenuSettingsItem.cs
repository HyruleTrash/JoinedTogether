using Godot;
using System;

/// <summary>
/// Triggers a pop up message to appear, saying that the settings aren't implemented yet
/// </summary>
public partial class MenuSettingsItem : MenuItem, IMenuItemTriggerable
{
    [Export] private Label NotAvailable;

    public void TriggerItem()
    {
        this.NotAvailable.Visible = true;
        Timer t = new();
        t.SetWaitTime(1);
        t.SetOneShot(true);
        this.AddChild(t);
        t.Start();
        t.Timeout += () =>
        {
            t.QueueFree();
            this.NotAvailable.Visible = false;
        };
    }
}
