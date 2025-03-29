using Godot;
using System;

/// <summary>
/// Triggers a pop up message to appear, saying that the settings aren't implemented yet
/// </summary>
public partial class MenuSettingsItem : MenuItem, IMenuItemTriggerable
{
    [Export]
    private Label _NotAvailable;

    public void TriggerItem()
    {
        this._NotAvailable.Visible = true;
        Timer t = new();
        t.SetWaitTime(1);
        t.SetOneShot(true);
        this.AddChild(t);
        t.Start();
        t.Timeout += () =>
        {
            t.QueueFree();
            this._NotAvailable.Visible = false;
        };
    }
}
