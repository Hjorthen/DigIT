using System;
using UnityEngine;


[Serializable]
public class WorldComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    private IOreFactory factory;
    void OnEnable()
    {
        factory = ServiceRegistry.GetService<IOreFactory>();

        var grid = new TileGrid(100, 100);
        for (uint i = 0; i < 100; i++)
        {
            for (uint j = 0; j < 100; j++)
            {
                grid[i, j] = new TileModel(new OreYield {OreType = GetRandomOre(), Quantity = 5}, 10); 
            }
        }
        var world = new World(grid, offset);
        ServiceRegistry.RegisterService(world);
        Debug.Log("Registered world");
    }

    private IOre GetRandomOre() {
        float sample = UnityEngine.Random.Range(0.0f, 1.0f);
        if(sample > 0.5f)
            return factory.GetOre(OreType.PLAIN);
        if(sample < 0.1f)
            return factory.GetOre(OreType.IRON);
        if(sample < 0.2f)
            return factory.GetOre(OreType.COAL);
         
        return factory.GetOre(OreType.COPPER);
    }
}
