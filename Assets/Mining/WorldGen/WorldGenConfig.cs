using UnityEngine;

[System.Serializable]
public struct OreChanceMarker {
    public OreType Type;
    public uint Depth;
    public uint Weight;
}

[System.Serializable]
[CreateAssetMenu(menuName = "WorldGen/OreDistributionConfig")]
public class WorldGenConfig : ScriptableObject
{
    public OreChanceMarker[] oreChances;
}
