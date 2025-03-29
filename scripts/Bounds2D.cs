using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// A utility class meant to holds 4 float values
/// </summary>
public class Bounds2D
{
    [Export]
    public Vector2 MinMaxX;
    [Export]
    public Vector2 MinMaxY;

    public Bounds2D()
    {
        this.MinMaxX = new();
        this.MinMaxY = new();
    }

    public Bounds2D(Vector2 minMaxX, Vector2 minMaxY)
    {
        this.MinMaxX = minMaxX;
        this.MinMaxY = minMaxY;
    }

    public Bounds2D(float minX, float maxX, float minY, float maxY)
    {
        this.MinMaxX = new(minX, maxX);
        this.MinMaxY = new(minY, maxY);
    }

    public static Bounds2D operator +(Bounds2D bounds, Vector2 offset)
    {
        return new Bounds2D(
            new(bounds.MinMaxX.X + offset.X, bounds.MinMaxX.Y + offset.X),
            new(bounds.MinMaxY.X + offset.Y, bounds.MinMaxY.Y + offset.Y)
        );
    }

    public void IsPointWithinBounds(Vector2 point, out bool insideX, out bool insideY)
    {
        if (point.X > this.MinMaxX.X && point.X < this.MinMaxX.Y)
            insideX = true;
        else
            insideX = false;

        if (point.Y > this.MinMaxY.X && point.Y < this.MinMaxY.Y)
            insideY = true;
        else
            insideY = false;
    }

    public bool IsPointWithinBounds(Vector2 point)
    {
        bool insideX;
        bool insideY;
        IsPointWithinBounds(point, out insideX, out insideY);

        if (insideX && insideY)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Calculates where the mid point is in between all bounds min max values
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMidPoint()
    {
        return new((this.MinMaxX.X + this.MinMaxX.Y) / 2, (this.MinMaxY.X + this.MinMaxY.Y) / 2);
    }

    public override string ToString()
    {
        return base.ToString() + "(" + this.MinMaxX.ToString() + ", " + this.MinMaxY.ToString() + ")";
    }
}