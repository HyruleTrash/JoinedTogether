using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class CameraBoundingBoxManager : Node
{
    [Export]
    public Array<CameraBoundingBox> BoundingBoxes = new();
    [Export]
    public Camera Camera;

    public Callable LoadBoundingBoxesButton => Callable.From(LoadBoundingBoxes);

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
        BoundingBoxes = new(list.ToArray());
    }
}