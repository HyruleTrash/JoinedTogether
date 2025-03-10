using Godot;
using System;
using System.Collections.Generic;

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
		TileSet = (TileSet)TileSet.Duplicate(); // make Tileset unique, so that the physics layer is not shared with all other Tilesets of the same kind
	}

	public void SetState(bool state)
	{
		if (state == false)
		{
			TileSet.SetPhysicsLayerCollisionLayer(0, CollisionLayerSleep);
			TileSet.SetPhysicsLayerCollisionMask(0, CollisionMaskSleep);
		}
		else
		{
			TileSet.SetPhysicsLayerCollisionLayer(0, CollisionLayerActive);
			TileSet.SetPhysicsLayerCollisionMask(0, CollisionMaskActive);
		}
	}
}
