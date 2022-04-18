using System;
using System.Collections.Generic;
using System.Linq;

public struct OreWeight {
    public readonly uint Weight;
    public readonly OreType Type;

    public OreWeight(uint weight, OreType type) : this()
    {
        Weight = weight;
        Type = type;
    }
}

public static class OreWeightSampleExtension
{
    public static OreType Sample(this IEnumerable<OreWeight> weights) {
        uint sum = weights.Where(w => w.Weight != 0).Aggregate((uint)0, (acc, sample) => acc + sample.Weight);
        
        var value = UnityEngine.Random.Range(0, sum);
        uint accumulator = 0;
        foreach(var sample in weights) {
            accumulator += sample.Weight;
            if(accumulator >= value) {
                return sample.Type;
            }
        }
        throw new Exception("We shouldn't get to here.");
    }
}

public interface IWeightedOreDistribution {
    public IEnumerable<OreWeight> GetWeightsAt(uint x, uint y);
}