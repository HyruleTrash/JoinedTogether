using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class MenuItem : HBoxContainer
{
    [Export]
    public Array<Label> Selectors = new Array<Label>();

    public void SetSelector(string newText)
    {
        for (int i = 0; i < Selectors.Count; i++)
        {
            Label label = Selectors[i];
            if (label != null)
                label.Text = newText;
        }
    }

    public virtual void TriggerItem()
    {
        return;
    }
}