using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeightedOreDistribution : IWeightedOreDistribution
{
    private readonly LinearOreChance[] weights;
    private readonly uint maxDepth;

    public WeightedOreDistribution(WorldGenConfig weights, uint maxDepth)
    {
        this.maxDepth = maxDepth;
        this.weights = weights.typedChances.GroupBy(t => t.Type, this.CreateLinearChanceForType).ToArray();
    }

    private LinearOreChance CreateLinearChanceForType(OreType type, IEnumerable<OreChanceMarkerTyped> chances) {
        return new LinearOreChance(type, chances.Select(v => new OrePoint(CalculateDepth(v.Depth), v.Weight)).OrderBy(p => p).ToList());
    }
    
    /// <summary>
    /// Translates the depth value in the terrain generation to be based on maxDepth. Allows the terrain generation to be entered as percentages,
    /// without having to consider the actual depth of the terrain.  
    /// </summary>
    private uint CalculateDepth(uint depth) {
        uint depthChance = (uint)(((float)depth / 1000.0) * maxDepth);
        return depthChance;
    }
    
    public IEnumerable<OreWeight> GetWeightsAt(uint x, uint y)
    {
        return weights.Select(s => new OreWeight(s.ChanceAt(y), s.Type));
    }
}
