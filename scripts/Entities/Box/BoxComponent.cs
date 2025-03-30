using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Base class for accessing the Box object as a component
/// </summary>
public partial class BoxComponent : Node
{
    protected Box _boxBody;

    public override void _Ready()
    {
        base._Ready();
        if (GetParent() is Box box)
        {
            this._boxBody = box;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (this._boxBody != null)
            _ProcessComponent(delta);
    }

    /// <summary>
    /// A custom process function only triggered when the _boxBody is set
    /// </summary>
    protected virtual void _ProcessComponent(double delta)
    {
        // Prefered virtual non implementation over, interface
    }
}