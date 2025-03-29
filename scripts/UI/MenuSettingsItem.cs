using Godot;
using System;

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
