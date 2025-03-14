using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Level : Node2D
{
    [Export]
    public Player Player;
    [Export]
    public Array<Terrain> StateGirlTerrain = new Array<Terrain>();
    [Export]
    public Array<Terrain> StateBoyTerrain = new Array<Terrain>();
    public SpawnPointManager SpawnPointManager;
    public CameraBoundingBoxManager CameraBoundingBoxManager;
    public Vector2 CurrentSpawnPoint;
    public GlobalData GlobalData;

    public override void _Ready()
    {
        base._Ready();
        Player = GetNode<Player>("Player");
        Player.OnStateSwitched += () => CorrectSetState();
        GlobalData = GetNode<GlobalData>("/root/GlobalData");
        _UpdateLevelData();
    }

    /// <summary>
    /// Updates all level data, and its dependant components
    /// </summary>
    private void _UpdateLevelData()
    {
        CorrectSetState();
        UpdateSpawnPoint();
        UpdateCameraBoundingBox();
    }

    /// <summary>
    /// Updates the current spawnpoint
    /// </summary>
    public void UpdateSpawnPoint()
    {
        if (SpawnPointManager == null || GlobalData == null)
            return;
        bool isInIndexRange;
        int offset = 1; // Offset is used to remove 0 indexing from level count
        Vector2 spawnPoint = SpawnPointManager.GetSpawnPoint(GlobalData.Level - offset, out isInIndexRange);
        if (isInIndexRange)
            CurrentSpawnPoint = spawnPoint;
        else
            GlobalData.MainMenu.TempIsOnEndScreen = true;
    }

    /// <summary>
    /// Updates the current camera bounding box
    /// </summary>
    public void UpdateCameraBoundingBox()
    {
        if (CameraBoundingBoxManager == null || GlobalData == null)
            return;

        int offset = 1; // Offset is used to remove 0 indexing from level count
        int index = GlobalData.Level - offset;
        if (index > CameraBoundingBoxManager.BoundingBoxes.Count - 1 || index < 0)
        {
            // GD.PushError("Index out of range");
            GlobalData.MainMenu.TempIsOnEndScreen = true;
            return;
        }
        CameraBoundingBox cameraBoundingBox = CameraBoundingBoxManager.BoundingBoxes[index];
        CameraBoundingBoxManager.Camera.BoundingBox = cameraBoundingBox.BoundingBox + cameraBoundingBox.Position;
        CameraBoundingBoxManager.Camera.FollowOffset = cameraBoundingBox.FollowOffset;
    }

    /// <summary>
    /// Delegate event is invoked the moment the player moves to the next sublevel
    /// </summary>
    public event Action NextLevelTriggered;

    /// <summary>
    /// Updates to the next subLevel
    /// </summary>
    public void SetNextLevel()
    {
        GlobalData.Level++;
        UpdateSpawnPoint();
        UpdateCameraBoundingBox();
        NextLevelTriggered.Invoke();
    }

    /// <summary>
    /// Switches the terrain used, based on the player state boy / girl
    /// Essentially correcting them based on the set state
    /// </summary>
    public void CorrectSetState()
    {
        if (Player.IsInGirlState == false)
        {
            _SetTerrainArrayState(StateGirlTerrain, true);
            _SetTerrainArrayState(StateBoyTerrain, false);
        }
        else
        {
            _SetTerrainArrayState(StateGirlTerrain, false);
            _SetTerrainArrayState(StateBoyTerrain, true);
        }
    }

    /// <summary>
    /// Sets all active/visible parameters of the terrains, inside the given array
    /// </summary>
    /// <param name="array">array containing all relevant terrains</param>
    /// <param name="state">the active state that you'd like to set them all to</param>
    private void _SetTerrainArrayState(Array<Terrain> array, bool state)
    {
        foreach (var terrain in array)
        {
            terrain.Visible = state;
            terrain.SetState(state);
        }
    }
}