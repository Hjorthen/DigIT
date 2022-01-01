using System;
using UnityEngine;


[Serializable]
public class WorldComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    void Awake()
    {
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
    }

    private Ore GetRandomOre() {
        float sample = UnityEngine.Random.Range(0.0f, 1.0f);
        if(sample > 0.5f)
            return new Ore { Name = "Plain" };
        if(sample < 0.1f)
            return new Ore { Name = "Iron" };
        if(sample < 0.2f)
            return new Ore { Name = "Coal" };
         
        return new Ore { Name = "Copper" };
    }
}
