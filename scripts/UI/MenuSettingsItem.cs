using Godot;
using System;

public partial class MenuSettingsItem : MenuItem
{
    [Export] private Label NotAvailable;

    public override void TriggerItem()
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
