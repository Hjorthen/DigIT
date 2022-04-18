using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct LinearOreChance {
    public readonly List<OrePoint> chances;
    public readonly OreType Type;

    public LinearOreChance(OreType type, List<OrePoint> chances) 
    {
        Type = type;
        this.chances = chances;
    }

    public uint ChanceAt(uint depth) {
        OrePoint start, end;
        for (int i = 0; i < chances.Count; i++) {
            if(chances[i].depth >= depth) {
                end = chances[i];
                int previousIndex = Math.Max(0, i - 1);
                start = chances[previousIndex];
                float interpolationRatio = (float)depth / end.depth;
                return (uint)Vector2.Lerp(new Vector2(start.chance, 0), new Vector2(end.chance, 0),  interpolationRatio).x;
            }
        }
        return chances.Last().chance;
    }
}
