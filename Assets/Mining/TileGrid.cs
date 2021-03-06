using System.Collections;
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
    public readonly TileGrid grid;
    public TileModel GetTileAt(Vector3 worldPosition) {
        var position = WorldToGridPosition(worldPosition);
        return grid[(uint)position.y, (uint)position.x];
    }

    public uint Height => grid.Height;
    public uint Width => grid.Width;

    public Vector2Int WorldToGridPosition(Vector3 worldPosition) {
        return new Vector2Int(Mathf.FloorToInt(worldPosition.x + offset.x), -Mathf.FloorToInt(worldPosition.y - offset.y));
    }

    public Vector3 GridToWorldPosition(int x, int y) {
        return new Vector3(Mathf.FloorToInt(x - offset.x), -Mathf.FloorToInt(y - offset.y));
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
