using System.Collections.Generic;
using UnityEngine;


public class World 
{
    public World(TileGrid grid, Vector3 offset)
    {
        this.grid = grid;
        this.offset = offset;
    }

    private Vector3 offset;
    private TileGrid grid;
    public TileModel GetTileAt(Vector3 worldPosition) {
        return grid[(uint)worldPosition.y + (uint)offset.y, (uint)worldPosition.x + (uint)offset.x];
    }
}

public class TileGrid 
{
    public TileGrid(uint height, uint width) {
        Height = height;
        Width = width;
    }
    private Dictionary<Vector2, TileModel> grid = new Dictionary<Vector2, TileModel>();

    public uint Height { get; }
    public uint Width { get; }

    // x for cache locality
    public TileModel this[uint y, uint x] {
        get {
            return grid[new Vector2Int((int)x, (int)y)];
        }
        set {
            var key = new Vector2Int((int)x, (int)y);
            if(grid.ContainsKey(key)) {
                throw new System.InvalidOperationException($"The grid already contains a tile at {key}");
            }
            grid[key] = value;
        }
    }
}
