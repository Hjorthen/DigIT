using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class LinearOreChanceTests
{
    [Test]
    public void ChanceInterpolatesBetweenTwoOrePoints() {
        OreType type = OreType.IRON;
        var chances = new List<OrePoint>() { new OrePoint(0, 0), new OrePoint(10, 10)};
        var linearChance = new LinearOreChance(type, chances);

        var expectedChance = 5;
        Assert.AreEqual(expectedChance, linearChance.ChanceAt(5));
    }

    [Test]
    public void SamplingPastLastLocationClampsToLastLocationChance() {
        OreType type = OreType.IRON;
        var chances = new List<OrePoint>() { new OrePoint(0, 0), new OrePoint(10, 10)};
        var linearChance = new LinearOreChance(type, chances);

        var expectedChance = 10;
        Assert.AreEqual(expectedChance, linearChance.ChanceAt(15));
    }

    [Test]
    public void SamplingWithOnePoint_ReturnsStaticChance() {
        OreType type = OreType.IRON;
        var chances = new List<OrePoint>() { new OrePoint(0, 5)};
        var linearChance = new LinearOreChance(type, chances);

        var expectedChance = 5;
        Assert.AreEqual(expectedChance, linearChance.ChanceAt(15));
    }
}
