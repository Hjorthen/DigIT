using System.Collections.Generic;
using System.Linq;

public class WeightedOreDistribution : IWeightedOreDistribution
{
    private readonly LinearOreChance[] weights;

    public WeightedOreDistribution(WorldGenConfig weights)
    {
        this.weights = weights.oreChances.GroupBy(t => t.Type, (key, val) => new LinearOreChance(key, val.Select(v => new OrePoint(v.Depth, v.Weight)).OrderBy(p => p).ToList())).ToArray();
    }
    
    public IEnumerable<OreWeight> GetWeightsAt(uint x, uint y)
    {
        return weights.Select(s => new OreWeight(s.ChanceAt(y), s.Type));
    }
}
