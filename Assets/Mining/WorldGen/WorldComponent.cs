using System;
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
    [SerializeField]
    private WorldGenConfig weights;
    private OreDistributionSampler oreDistribution;
    void OnEnable()
    {
        factory = ServiceRegistry.GetService<IOreFactory>();
        uint width = 18;
        uint height = 60;
        oreDistribution = new WeightedOreDistributionSampler(new WeightedOreDistribution(weights));
        TileGrid grid = GenerateWorldGrid(width, height);

        world = new World(grid, offset);
        ServiceRegistry.RegisterService(world);
    }

    private TileGrid GenerateWorldGrid(uint width, uint height)
    {
        var grid = new TileGrid(height, width);
        for (uint i = 0; i < height; i++)
        {
            for (uint j = 0; j < width; j++)
            {
                var oreType = oreDistribution.SampleAt(j, i);
                grid[i, j] = new TileModel(new OreYield { OreType = factory.GetOre(oreType), Quantity = 5 }, 10);
            }
        }

        return grid;
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
