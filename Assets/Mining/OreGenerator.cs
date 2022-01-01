using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    private TileGrid grid;

    // Start is called before the first frame update
    void Start()
    {
        for(uint y=0;y<grid.Height;++y) {
            for(uint x=0;x<grid.Width;++x) {
                var tile = grid[y, x];
                var ore = tile.Ore;
                ore.OreType = new Ore { Name = "Copper" };
            }
        }
    }
}
