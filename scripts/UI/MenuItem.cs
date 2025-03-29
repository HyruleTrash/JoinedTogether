using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// A class meant to be inherited. adding logic for making a item selectable
/// </summary>
public partial class MenuItem : HBoxContainer
{
    [Export]
    public Array<Label> Selectors = new Array<Label>();

    public void SetSelector(string newText)
    {
        for (int i = 0; i < this.Selectors.Count; i++)
        {
            Label label = this.Selectors[i];
            if (label != null)
                label.Text = newText;
        }
    }
}