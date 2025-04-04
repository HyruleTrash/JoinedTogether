using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// For logic relative to tracking which sublevel the box is in
/// </summary>
public partial class BoxLevelTracker : BoxComponent
{
    private Level _level;
    public Level Level
    {
        set
        {
            this._level = value;
        }
        get
        {
            if (this._level == null && this._boxBody != null)
            {
                this._level = this._boxBody.GetNode<Level>("../../../");
            }
            return this._level;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        if (this._boxBody != null)
            this._boxBody.BoxLevelTracker = this;
    }

    /// <summary>
    /// Checks if the box is currently inside the sublevel that the player is also in
    /// </summary>
    /// <returns></returns>
    public bool IsInsideCurrentSubLevel()
    {
        return this.Level.CameraBoundingBoxManager.Camera.BoundingBox.IsPointWithinBounds(this._boxBody.BoxRespawner.GetSpawnPoint());
    }
}