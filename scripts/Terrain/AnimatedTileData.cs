using Godot;
using System;
using System.Collections.Generic;

class AnimatedTileData
{
    public bool AnimateForwards = true;
    public string RecourceName = "";
    public int StartTile = 0;
    public int EndTile = 0;
    public bool PingPong = false;

    public AnimatedTileData(string RecourceName, int StartTile, int EndTile, bool PingPong = false)
    {
        this.RecourceName = RecourceName;
        this.StartTile = StartTile;
        this.EndTile = EndTile;
        this.PingPong = PingPong;
    }
}