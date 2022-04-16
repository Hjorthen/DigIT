using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

public struct OreWeight {
    public readonly uint Weight;
    public readonly OreType Type;

    public OreWeight(uint weight, OreType type) : this()
    {
        Weight = weight;
        Type = type;
    }
}

public static class WeightedOreDistributionSampler
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

public interface OreDistributionSampler {
    public OreType SampleAt(uint x, uint y);
}

public class TestOreDistributionSampler : OreDistributionSampler
{
    private IWeightedOreDistribution distribution;

    public TestOreDistributionSampler(IWeightedOreDistribution distribution)
    {
        this.distribution = distribution;
    }

    public OreType SampleAt(uint x, uint y)
    {
        var samples = distribution.GetWeightsAt(x, y);
        return WeightedOreDistributionSampler.Sample(samples);
    }
}