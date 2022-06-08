using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class WeightedOreDistributionTests 
{
    [Test]
    public void WhenSampled_ReturnsSampleForEveryType() {
        WorldGenConfig config = new WorldGenConfig();
        config.oreChances = new OreChanceMarker[] {
            new OreChanceMarker { Type = "IRON" },
            new OreChanceMarker { Type = "COPPER" },
        };
        WeightedOreDistribution distribution = new WeightedOreDistribution(config, 100);

        var result = distribution.GetWeightsAt(0, 0).Select(w => w.Type).ToHashSet();

        var expectedTypes = new HashSet<OreType>() { OreType.IRON, OreType.COPPER};
        Assert.AreEqual(result, expectedTypes);
    }
}