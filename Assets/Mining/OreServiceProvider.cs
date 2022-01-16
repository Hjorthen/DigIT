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

public interface IPriceProvider {
    ISellableItem FromOre(OreType type);
}

[System.Serializable]
public class OreServiceProvider : MonoBehaviour, IOreFactory, OreToVisualizerMapper, IPriceProvider
{
    [SerializeField]
    private List<Ore> Ores;

    public ISellableItem FromOre(OreType type)
    {
        return Ores.First(o => o.Type == type);
    }

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
        ServiceRegistry.RegisterService<IPriceProvider>(this);
    }
}
