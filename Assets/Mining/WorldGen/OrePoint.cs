using System;

public struct OrePoint : IComparable<OrePoint> {
    public readonly uint depth;
    public readonly uint chance;

    public OrePoint(uint depth, uint chance)
    {
        this.depth = depth;
        this.chance = chance;
    }

    public int CompareTo(OrePoint other)
    {
        return this.depth.CompareTo(other.depth);
    }

    public static bool operator > (OrePoint op1, OrePoint op2) 
    {
        return op1.CompareTo(op2) > 0;
    }
    public static bool operator < (OrePoint op1, OrePoint op2) 
    {
        return op1.CompareTo(op2) < 0;
    }

    public static bool operator >= (OrePoint op1, OrePoint op2) 
    {
        return op1.CompareTo(op2) >= 0;
    }

    public static bool operator <= (OrePoint op1, OrePoint op2) 
    {
        return op1.CompareTo(op2) <= 0;
    }
}
