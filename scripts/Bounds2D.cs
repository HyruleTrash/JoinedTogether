using Godot;
using System;
using System.Collections.Generic;

public class Bounds2D
{
    [Export]
    public Vector2 MinMaxX;
    [Export]
    public Vector2 MinMaxY;

    public Bounds2D()
    {
        MinMaxX = new();
        MinMaxY = new();
    }

    public Bounds2D(Vector2 minMaxX, Vector2 minMaxY)
    {
        this.MinMaxX = minMaxX;
        this.MinMaxY = minMaxY;
    }

    public Bounds2D(float minX, float maxX, float minY, float maxY)
    {
        MinMaxX = new(minX, maxX);
        MinMaxY = new(minY, maxY);
    }

    public static Bounds2D operator +(Bounds2D bounds, Vector2 offset)
    {
        return new Bounds2D(
            bounds.MinMaxX + offset,
            bounds.MinMaxY + offset
        );
    }

    public void IsPointWithinBounds(Vector2 point, out bool insideX, out bool insideY)
    {
        if (point.X > MinMaxX.X && point.X < MinMaxX.Y)
            insideX = true;
        else
            insideX = false;

        if (point.Y > MinMaxY.X && point.Y < MinMaxY.Y)
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
        return new((MinMaxX.X + MinMaxX.Y) / 2, (MinMaxY.X + MinMaxY.Y) / 2);
    }

    public override string ToString()
    {
        return base.ToString() + "(" + MinMaxX.ToString() + ", " + MinMaxY.ToString() + ")";
    }
}