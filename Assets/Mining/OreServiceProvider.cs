using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum OreType {
    COPPER,
    IRON,
    COAL,
    PLAIN
}

public interface IOreFactory {
    IOre GetOre(OreType type);
}

[System.Serializable]
public class OreServiceProvider : MonoBehaviour, IOreFactory, OreToVisualizerMapper
{
    [SerializeField]
    private List<Ore> Ores;

    public IOre GetOre(OreType type)
    {
        return Ores.First(o => o.Type == type);
    }

    public GameObject GetPrefabFor(IOre ore)
    {
        return Ores.First(o => o.Type == ore.Type).GetTilePrefab();
    }

    void Awake()
    {
        ServiceRegistry.RegisterService<IOreFactory>(this);
        ServiceRegistry.RegisterService<OreToVisualizerMapper>(this);
    }
}
