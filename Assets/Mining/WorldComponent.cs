using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class WorldComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    private IOreFactory factory;
    private World world;
    [SerializeField]
    private GameObject tilePrefab;
    void OnEnable()
    {
        factory = ServiceRegistry.GetService<IOreFactory>();
        uint width = 18;
        uint height = 60;
        var grid = new TileGrid(height, width);
        for (uint i = 0; i < height; i++)
        {
            for (uint j = 0; j < width; j++)
            {
                grid[i, j] = new TileModel(new OreYield {OreType = GetRandomOre(), Quantity = 5}, 10); 
            }
        }

        world = new World(grid, offset);
        ServiceRegistry.RegisterService(world);
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

    void Update() 
    {
        var registry = ServiceRegistry.GetService<TileRegistry>();
        for(uint y = 0;y<world.Height;++y) {
            for(uint x = 0;x<world.Width;++x) {
                var tile = registry.GetTile((int)x, (int)y);
                if(tile == null)
                    CreateTileFor(x, y);
            }
        }
        this.enabled = false;
    }

    private void CreateTileFor(uint x, uint y)
    {
        var tileAtLocation = world.grid[y, x];
        var worldPosition = world.GridToWorldPosition((int)x, (int)y);
        GameObject.Instantiate(tilePrefab, worldPosition, Quaternion.identity);        
    }
}
