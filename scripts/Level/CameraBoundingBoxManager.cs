using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Holds all the CameraBoundingBoxes per sublevel
/// </summary>
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
    /// Returns the CameraBoundingBox relative to the given sublevel
    /// </summary>
    /// <param name="givenIndex">sublevel index</param>
    /// <param name="isInIndexRange">bool representing if the BoundingBox actually exists or not</param>
    /// <returns>The spawn location</returns>
    public CameraBoundingBox GetBoundingBox(int givenIndex, out bool isInIndexRange)
    {
        int offset = 1; // Offset is used to remove 0 indexing from level count
        int index = givenIndex - offset;
        if (index < 0 || index > this.BoundingBoxes.Count - offset)
        {
            isInIndexRange = false;
            return null;
        }

        isInIndexRange = true;
        return this.BoundingBoxes[index];
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