using System;
using System.Collections.Generic;
using UnityEngine;

public interface OreDistribution
{
    IOre GetOreFor(uint x, uint y);
}

public struct OrePoint {
    public readonly uint depth;
    public readonly uint chance;

    public OrePoint(uint depth, uint chance)
    {
        this.depth = depth;
        this.chance = chance;
    }
}

public struct OreChance {
    private readonly List<OrePoint> chances;
    public readonly OreType Type;

    public OreChance(OreType type, List<OrePoint> chances) 
    {
        Type = type;
        this.chances = chances;
    }

    public uint ChanceAt(uint depth) {
        return chances[0].chance;
    }
}

public class TestDistribution : OreDistribution
{
    private IOreFactory oreFactory;

    public TestDistribution(IOreFactory oreFactory)
    {
        this.oreFactory = oreFactory;
    }

    private OreChance iron = new OreChance(OreType.IRON, new List<OrePoint>() { new OrePoint(0, 100)});
    private OreChance copper = new OreChance(OreType.COPPER, new List<OrePoint>() { new OrePoint(0, 100)});

    public IOre GetOreFor(uint x, uint y)
    {
        if(x < 5)
            return oreFactory.GetOre(OreType.IRON);
        return oreFactory.GetOre(OreType.COPPER);
    }
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
    private OreDistribution oreDistribution;
    void OnEnable()
    {
        factory = ServiceRegistry.GetService<IOreFactory>();
        uint width = 18;
        uint height = 60;
        oreDistribution = new TestDistribution(factory);
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
                grid[i, j] = new TileModel(new OreYield { OreType = oreDistribution.GetOreFor(j, i), Quantity = 5 }, 10);
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
