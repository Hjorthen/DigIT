using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct LineSegment {
    public Vector2 Start;
    public Vector2 End;
    public bool Contains(int x, int y) {
        return (x >= Start.x && x <= End.x) && (y >= Start.y && y <= End.y);
    }
}

[CreateAssetMenu(menuName = "POI/Generator")]
public class PointOfInterestGenerator : ScriptableObject, ITunnelGenerator {

    [SerializeField]
    private PrefabFeature firstTunnelNPC;    
    [SerializeField]
    private PrefabFeature secondTunnelNPC;    
    [SerializeField]
    private PrefabFeature refuelTutorialNPC;
    [SerializeField]
    private PrefabFeature harderGroundDialogue;

    [SerializeField]
    private List<PrefabFeature> _StoryBeacons; 
    private List<LineSegment> tunnels;
    private List<TunnelFeature> _TunnelFeatures;
    // Kept seperate from tunnels since we dont want to spawn anything inside it which will just fall to the bottom
    private LineSegment shaft;
    private Vector2 gameObjectFeatureLocation;

    void OnEnable() {
        shaft = new LineSegment { Start = new Vector2(10, 0), End = new Vector2(10, 22) };
        tunnels = GenerateTunnels();
        _TunnelFeatures = GenerateTunnelFeatures();
        _TunnelFeatures = AddStoryBeacons(_TunnelFeatures, _StoryBeacons);
    }

    private List<TunnelFeature> AddStoryBeacons(List<TunnelFeature> tunnelFeatures, List<PrefabFeature> storyBeacons)
    {
        for (int i = 0; i < storyBeacons.Count; i++) {
            tunnelFeatures.Add(new TunnelFeature() {Feature = storyBeacons[i], Location = new Vector2(14, 50* (i + 1))} ); // Adds a story beacon at every 50 tile interval
        }
        return tunnelFeatures;
    }

    public bool GetTunnelAt(uint x, uint y) {
        return tunnels.Any(t => t.Contains((int)x, (int)y)) || shaft.Contains((int)x, (int)y);
    }

    private List<LineSegment> GenerateTunnels() {
        // The minimum distance on the Y-axis between tunnels
        int minTunnelSeperation = 3;
        int maxTunnelSeperation = 7;
        int minTunnelLength = 20;
        int maxTunnelLength = 35;
        int minTunnelOffset = 1;
        int maxTunnelOffset = 6;
        int tunnelStopDepth = 64;

        var generatedTunnels = new List<LineSegment>();
        do {
            int latestTunnelDepth =  0;
            if (generatedTunnels.Count > 0)
                latestTunnelDepth = (int)generatedTunnels.Last().End.y;

            int nextTunnelDepth = latestTunnelDepth + Random.Range(minTunnelSeperation, maxTunnelSeperation);
            int nextTunnelLength = Random.Range(minTunnelLength, maxTunnelLength);
            int nextTunnelOffset = Random.Range(minTunnelOffset, maxTunnelOffset);

            LineSegment nextTunnel = new LineSegment() { Start = new Vector2(nextTunnelOffset, nextTunnelDepth), End = new Vector2(nextTunnelOffset + nextTunnelLength, nextTunnelDepth) };
            generatedTunnels.Add(nextTunnel); 
        } while(generatedTunnels.Last().Start.y + minTunnelSeperation < tunnelStopDepth);

        return generatedTunnels;
    }

    private class TunnelFeature {
        public Vector2 Location;
        public PrefabFeature Feature;
    }

    private List<TunnelFeature> GenerateTunnelFeatures() {
        int shaftLocationX = (int)shaft.Start.x;
        var spawnLocationGenerator = new TunnelRandomSpawnLocationAroundShaftGenerator(shaftLocationX, new RandomNumberGenerator());
        // Spawn an NPC right near the shaft in first tunnel
        var firstEncounter = new TunnelFeature {
            Feature = firstTunnelNPC,
            Location = spawnLocationGenerator.GetSpawnLocationInTunnel(tunnels.First(), 5, 10)
        };

        var secondEncounter = new TunnelFeature {
            Feature = secondTunnelNPC,
            Location = spawnLocationGenerator.GetSpawnLocationInTunnel(tunnels[1], 8, 10)
        };

        var refuelingEncounter = new TunnelFeature {
            Feature = refuelTutorialNPC,
            Location = spawnLocationGenerator.GetSpawnLocationInTunnel(FindClosestTunnelToDepth(tunnels, 22), 2, 4)
        };

        var harderGroundDialogueEncounter = new TunnelFeature {
            Feature = harderGroundDialogue,
            Location = spawnLocationGenerator.GetSpawnLocationInTunnel(FindClosestTunnelToDepth(tunnels, 40), 2, 5)
        };

        var tunnelFeatures = new List<TunnelFeature>() {
            firstEncounter,
            secondEncounter,
            refuelingEncounter,
            harderGroundDialogueEncounter
        };

        return tunnelFeatures;
    }
    /// <summary>
    /// Assumes that "tunnels" are ordered by depth 
    /// </summary>
    /// <returns>
    /// The tunnel closest to depth
    /// </returns>
    private static LineSegment FindClosestTunnelToDepth(List<LineSegment> tunnels, int depth) {
        for (int i = 1; i < tunnels.Count; i++) {
            int previousTunnelDepth = (int)tunnels[i - 1].Start.y;
            int currentTunnelDepth = (int)tunnels[i].Start.y;

            int previousTunnelDistance = Mathf.Abs(depth - previousTunnelDepth);
            int currentTunnelDistance = Mathf.Abs(depth - currentTunnelDepth);

            if(currentTunnelDistance >= previousTunnelDistance) {
                return tunnels[i - 1];
            } 
        }
        return tunnels.Last();
    }

    public PrefabFeature GetFeatureAt(uint x, uint y)
    {
        return _TunnelFeatures.FirstOrDefault(t => t.Location == new Vector2(x, y))?.Feature; 
    }

}

interface ITunnelSpawnLocationStrategy {
    public Vector2 GetSpawnLocationInTunnel(LineSegment tunnel, int minDistance, int maxDistance);
}

public class TunnelRandomSpawnLocationAroundShaftGenerator : ITunnelSpawnLocationStrategy
{
    // The X-coordinate at which the world entry-shaft is located
    private int shaftLocationX;

    // Used to determine on which side the spawn location should be of the shaft location
    private INumberGenerator numberGenerator;

    public enum TUNNEL_SPAWNING_SIDE {
        SPAWN_LEFT,
        SPAWN_RIGHT
    }

    public TunnelRandomSpawnLocationAroundShaftGenerator(int shaftLocationX, INumberGenerator numberGenerator)
    {
        this.shaftLocationX = shaftLocationX;
        this.numberGenerator = numberGenerator;
    }

    public Vector2 GetSpawnLocationInTunnel(LineSegment tunnel, int minDistance, int maxDistance)
    {
        // We are spawning "around" the shaft, so we add one to the min distance to avoid spawning directly in the shaft
        minDistance += 1;

        // Roll wheter we should spawn to the left of the tunnel or not
        bool spawnLeft = DetermineSpawnLeft(tunnel, minDistance);

        int tunnelLengthLeft = Mathf.Min(shaftLocationX - (int)tunnel.Start.x, maxDistance);
        int tunnelLengthRight = Mathf.Min((int)tunnel.End.x - shaftLocationX, maxDistance);

        int spawnLocationX;
        if(spawnLeft) {
            int offset = GetRandomPointAlongAngle(Mathf.PI, minDistance, tunnelLengthLeft);
            spawnLocationX = shaftLocationX + offset;
        } else {
            int offset = GetRandomPointAlongAngle(0, minDistance, tunnelLengthRight);
            spawnLocationX = shaftLocationX + offset;
        }

        return new Vector2(spawnLocationX, tunnel.Start.y);
    }

    private bool DetermineSpawnLeft(LineSegment tunnel, int minDistance) {
        int roll = numberGenerator.Range(0, 2);

        if(shaftLocationX - tunnel.Start.x < minDistance) {
            // We cannot satisfy the min distance requirement, try the other direction
            roll = (int)TUNNEL_SPAWNING_SIDE.SPAWN_RIGHT;
        }

        if(tunnel.End.x - shaftLocationX < minDistance) {
            // min distance cannot be satisfied for the given tunnel
            throw new System.ArgumentException("Cannot satisfy min distance for the given tunnel", nameof(minDistance));
        }
        return roll == (int)TUNNEL_SPAWNING_SIDE.SPAWN_LEFT;
    }

    private int GetRandomPointAlongAngle(float radDirection, int minDistance, int maxDistance) {
        return (int)(numberGenerator.Range(minDistance, maxDistance + 1) * Mathf.Cos(radDirection));
    }
}