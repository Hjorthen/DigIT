using NUnit.Framework;

class TunnelRandomSpawnLocationAroundShaftGeneratorTests {
    private MockNumberGenerator numberGenerator;
    private int shaftLocationX;
    private TunnelRandomSpawnLocationAroundShaftGenerator generator;


    [SetUp]
    public void Setup() {
        shaftLocationX = 10; 
        numberGenerator = new MockNumberGenerator(0, 0);
        generator = new TunnelRandomSpawnLocationAroundShaftGenerator(shaftLocationX, numberGenerator);
    }

    [TestCase(TunnelRandomSpawnLocationAroundShaftGenerator.TUNNEL_SPAWNING_SIDE.SPAWN_LEFT, -4)]
    [TestCase(TunnelRandomSpawnLocationAroundShaftGenerator.TUNNEL_SPAWNING_SIDE.SPAWN_RIGHT, +4)]
    public void SpawnLocation_Is_ExptedSideOfTunnel(int spawnDecisionRoll, int expectedShaftOffset) {
        var tunnel = CreateHorizontalTunnelAround(shaftLocationX);
        numberGenerator.SpawnDecisionRoll = spawnDecisionRoll;
        numberGenerator.FixedDecisionRoll = 4;

        var location = generator.GetSpawnLocationInTunnel(tunnel, 0, 4);

        Assert.AreEqual(shaftLocationX + expectedShaftOffset, location.x);
    }

    [Test]
    public void MinDistance_IsIncrementedByOne_ToPreventSpawningInTunnel() {
        var tunnel = CreateHorizontalTunnelAround(shaftLocationX);
        const int minDistance = 0;
        
        // We are only testing that we do increment the minimum distance passed to the number generator by one
        // Hence the other params does not matter
        generator.GetSpawnLocationInTunnel(tunnel, minDistance, 0);

        Assert.AreEqual(minDistance + 1, numberGenerator.lastMinParam);
    }

    [Test]
    public void MaxDistance_IsIncrementedByOne_ToAccomodateForGeneratorNumberExclusiveBehavior() {
        var tunnel = CreateHorizontalTunnelAround(shaftLocationX);
        const int maxDistance = 0;
        
        generator.GetSpawnLocationInTunnel(tunnel, 0, maxDistance);

        Assert.AreEqual(maxDistance + 1, numberGenerator.lastMinParam);
    }

    [TestCase(TunnelRandomSpawnLocationAroundShaftGenerator.TUNNEL_SPAWNING_SIDE.SPAWN_LEFT, 5, 10, 5)]
    [TestCase(TunnelRandomSpawnLocationAroundShaftGenerator.TUNNEL_SPAWNING_SIDE.SPAWN_RIGHT, 5, 10, 5)]
    public void SpawnLocation_DoesNotExceed_TunnelLength(int spawnDirection, int tunnelRangeAroundCenter, int maxDistanceFromShaft, int expectedMaxDistance) {
        var tunnel = CreateHorizontalTunnelAround(shaftLocationX, tunnelRangeAroundCenter);

        var location = generator.GetSpawnLocationInTunnel(tunnel, 0, maxDistanceFromShaft);

        Assert.AreEqual(expectedMaxDistance + 1, numberGenerator.lastMaxParam);
    }

    [TestCase(TunnelRandomSpawnLocationAroundShaftGenerator.TUNNEL_SPAWNING_SIDE.SPAWN_LEFT)]
    [TestCase(TunnelRandomSpawnLocationAroundShaftGenerator.TUNNEL_SPAWNING_SIDE.SPAWN_RIGHT)]
    public void WhenMinDistance_ExceedsOneSideOfTunnel_TheOtherSideIsSelected(int expectedTunnel) {
        int tunnelRadiusFromCenter = 5;
        int tunnelCenter = 10;
        LineSegment tunnel;
        if(expectedTunnel == (int)TunnelRandomSpawnLocationAroundShaftGenerator.TUNNEL_SPAWNING_SIDE.SPAWN_RIGHT) {
            tunnel = CreateHorizontalTunnelAround(tunnelCenter, 5, 10);
        } else {
            tunnel = CreateHorizontalTunnelAround(tunnelCenter, 10, 5);
        }
    }
    
    private LineSegment CreateHorizontalTunnelAround(int center, int tunnelRadiusFromCenter = 5) {
        return new LineSegment { Start = new UnityEngine.Vector2(center - tunnelRadiusFromCenter, 0), End = new UnityEngine.Vector2(center + tunnelRadiusFromCenter, 0)};
    }

    private LineSegment CreateHorizontalTunnelAround(int center, int lengthLeft, int lengthRight) {
        return new LineSegment { Start = new UnityEngine.Vector2(center - lengthLeft, 0), End = new UnityEngine.Vector2(center + lengthRight, 0)};
    }


    // The TunnelRandomSpawnLocationGenerator rolls twice: Once for determining whether to return
    // a location to the left of the shaft or the right. We use this Mock to differentiate between
    // the shaft roll and distance roll
    private class MockNumberGenerator : INumberGenerator
    {
        // The generator returns this roll first to accomodate for the spawn left or right roll
        public int SpawnDecisionRoll;
        public int FixedDecisionRoll;
        public bool ReturnSpawnDecisionRoll;

        public int lastMinParam {
            get; 
            private set;
        }
        public int lastMaxParam {
            get;
            private set;
        }

        public MockNumberGenerator(int spawnDecisionRoll, int fixedDecisionRoll)
        {
            SpawnDecisionRoll = spawnDecisionRoll;
            FixedDecisionRoll = fixedDecisionRoll;
            ReturnSpawnDecisionRoll = true;
        }

        public int Range(int minInclusive, int maxExclusive)
        {
            lastMinParam = minInclusive;
            lastMaxParam = maxExclusive;

            if (ReturnSpawnDecisionRoll) {
                ReturnSpawnDecisionRoll = false;
                return SpawnDecisionRoll;
            }
            return FixedDecisionRoll;
        }
    }   
}