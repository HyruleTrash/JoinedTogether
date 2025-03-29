using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the logic for switching a tilemap's state, or layout (two are required)
/// </summary>
public partial class Terrain : CustomTileMapLayer
{
	[Export]
	public AnimatedTiles AnimatedTiles;

	[ExportGroup("Active state")]
	[ExportSubgroup("Collision")]
	[Export(PropertyHint.Layers2DPhysics)]
	public uint CollisionLayerActive;
	[Export(PropertyHint.Layers2DPhysics)]
	public uint CollisionMaskActive;

	[ExportGroup("Sleep state")]
	[ExportSubgroup("Collision")]
	[Export(PropertyHint.Layers2DPhysics)]
	public uint CollisionLayerSleep;
	[Export(PropertyHint.Layers2DPhysics)]
	public uint CollisionMaskSleep;

	public override void _Ready()
	{
		base._Ready();
		this.TileSet = (TileSet)this.TileSet.Duplicate(); // make Tileset unique, so that the physics layer is not shared with all other Tilesets of the same kind
		SetState(true);
	}

	public void SetState(bool state)
	{
		if (state == false)
		{
			this.TileSet.SetPhysicsLayerCollisionLayer(0, CollisionLayerSleep);
			this.TileSet.SetPhysicsLayerCollisionMask(0, CollisionMaskSleep);
		}
		else
		{
			this.TileSet.SetPhysicsLayerCollisionLayer(0, CollisionLayerActive);
			this.TileSet.SetPhysicsLayerCollisionMask(0, CollisionMaskActive);
		}
	}
}
