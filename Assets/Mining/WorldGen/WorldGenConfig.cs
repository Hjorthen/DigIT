using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct OreChanceMarker {
    public string Type;
    public uint Depth;
    public uint Weight;
}

public struct OreChanceMarkerTyped {
    public OreType Type;
    public uint Depth;
    public uint Weight;
}

[System.Serializable]
[CreateAssetMenu(menuName = "WorldGen/OreDistributionConfig")]
public class WorldGenConfig : ScriptableObject
{
    public OreChanceMarker[] oreChances;
    public IEnumerable<OreChanceMarkerTyped> typedChances => oreChances.Select((m) => new OreChanceMarkerTyped() { Type = Enum.Parse<OreType>(m.Type), Depth = m.Depth, Weight = m.Weight});
}
