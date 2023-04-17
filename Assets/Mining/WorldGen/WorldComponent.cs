using System;
using UnityEngine;


public interface ITunnelGenerator {
    public bool GetTunnelAt(uint x, uint y);
}

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
    [SerializeField]
    private PointOfInterestGenerator pointOfInterestGenerator;
    void OnEnable()
    {
        factory = ServiceRegistry.GetService<IOreFactory>();
        uint width = 32;
        uint height = 500;
        oreDistribution = new WeightedOreDistributionSampler(new WeightedOreDistribution(weights, height));
        TileGrid grid = GenerateWorldGrid(width, height);

        world = new World(grid, offset);
        ServiceRegistry.RegisterService(world);
    }

    private TileGrid GenerateWorldGrid(uint width, uint height)
    {
        var grid = new TileGrid(height, width);
        ITunnelGenerator tunnelGenerator = pointOfInterestGenerator;
        for (uint i = 0; i < height; i++)
        {
            for (uint j = 0; j < width; j++)
            {
                if(!tunnelGenerator.GetTunnelAt(j, i)) {
                    var oreType = oreDistribution.SampleAt(j, i);
                    grid[i, j] = new TileModel(new OreYield { OreType = factory.GetOre(oreType), Quantity = 1 }, GetTileLifeAtDepth(i));
                } else {
                    grid[i, j] = null;
                }
            }
        }

        return grid;
    }

    private float GetTileLifeAtDepth(uint depth) {
        if(depth <= 45) {
            return 200;
        }
        
        return 200 + depth;
    }
    
    void Update() 
    {
        var registry = ServiceRegistry.GetService<TileRegistry>();
        for(uint y = 0;y<world.Height;++y) {
            for(uint x = 0;x<world.Width;++x) {
                var feature = pointOfInterestGenerator.GetFeatureAt(x, y);
                if(feature == null)
                {
                    TrySpawnTile(registry, y, x);
                } else {
                    SpawnFeatureFromGridCoordinates(y, x, feature);
                }
            }
        }
        this.enabled = false;
    }

    private void SpawnFeatureFromGridCoordinates(uint y, uint x, PrefabFeature feature)
    {
        var worldPosition = world.GridToWorldPosition((int)x, (int)y);
        feature.SpawnAt(worldPosition);
    }

    private void TrySpawnTile(TileRegistry registry, uint y, uint x)
    {
        var tile = registry.GetTile((int)x, (int)y);
        if (tile == null)
            CreateTileFromGridCoordinates(x, y);
    }

    private void CreateTileFromGridCoordinates(uint x, uint y)
    {
        var tileAtLocation = world.grid[y, x];
        var worldPosition = world.GridToWorldPosition((int)x, (int)y);
        GameObject.Instantiate(tilePrefab, worldPosition, Quaternion.identity);        
    }
}
