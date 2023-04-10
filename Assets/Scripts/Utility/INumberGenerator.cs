// Utility for getting a number within the specified range
using UnityEngine;

public interface INumberGenerator {
    int Range(int minInclusive, int maxExclusive);
}

public class RandomNumberGenerator : INumberGenerator
{
    public int Range(int minInclusive, int maxExclusive)
    {
        return Random.Range(minInclusive, maxExclusive);
    }
}

/// <summary>
/// A number generator which always returns the same result 
/// </summary>
public class FixedNumberGenerator : INumberGenerator
{
    public int FixedResult {
        get;
        set;
    }

    public FixedNumberGenerator(int fixedResult)
    {
        this.FixedResult = fixedResult;
    }

    public int Range(int minInclusive, int maxExclusive)
    {
        return FixedResult;
    }
}