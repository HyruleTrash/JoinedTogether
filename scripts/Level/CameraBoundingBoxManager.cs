using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class CameraBoundingBoxManager : Node
{
    [Export]
    public Array<CameraBoundingBox> BoundingBoxes = new(); // this array is used and accesed using the sub level index
    [Export]
    public Camera Camera;

    public override void _Ready()
    {
        base._Ready();

        LoadBoundingBoxes();
        Level level = GetNode<Level>("../");
        if (level != null)
        {
            level.CameraBoundingBoxManager = this;
            level.UpdateCameraBoundingBox();
        }
    }

    /// <summary>
    /// Loads the Camera bounding boxes into an array.
    /// </summary>
    public void LoadBoundingBoxes()
    {
        List<CameraBoundingBox> list = new();
        foreach (Node item in GetChildren())
        {
            if (item is CameraBoundingBox)
            {
                list.Add((CameraBoundingBox)item);
            }
        }
        this.BoundingBoxes = new(list.ToArray());
    }
}