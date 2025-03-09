using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Level : Node
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
    public GlobalData _globalData;

    public override void _Ready()
    {
        base._Ready();
        Player = GetNode<Player>("Player");
        Player.OnStateSwitched += () => CorrectSetState();
        _globalData = GetNode<GlobalData>("/root/GlobalData");
        UpdateLevelData();
    }

    /// <summary>
    /// Updates all level data, and its dependant components
    /// </summary>
    private void UpdateLevelData()
    {
        CorrectSetState();

        // Update spawnpoint
        bool isInIndexRange;
        int offset = 1; // Offset is used to remove 0 indexing from level count
        Vector2 spawnPoint = SpawnPointManager.GetSpawnPoint(_globalData.Level - offset, out isInIndexRange);
        GD.Print(spawnPoint, ", ", isInIndexRange);
        if (isInIndexRange)
            CurrentSpawnPoint = spawnPoint;
        else
            _globalData.MainMenu.TempIsOnEndScreen = true;

        // Update Cam boundingbox
        CameraBoundingBoxManager.Camera.BoundingBox = CameraBoundingBoxManager.BoundingBoxes[_globalData.Level - offset].BoundingBox;
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