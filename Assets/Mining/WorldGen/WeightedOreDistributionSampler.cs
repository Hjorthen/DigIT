public class WeightedOreDistributionSampler : OreDistributionSampler
{
    private IWeightedOreDistribution distribution;

    public WeightedOreDistributionSampler(IWeightedOreDistribution distribution)
    {
        this.distribution = distribution;
    }

    public OreType SampleAt(uint x, uint y)
    {
        var samples = distribution.GetWeightsAt(x, y);
        return samples.Sample();
    }
}