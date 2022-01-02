using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    private TileGrid grid;
    private IOreFactory oreFactory;

    // Start is called before the first frame update
    void Start()
    {
        oreFactory = ServiceRegistry.GetService<IOreFactory>();

        for(uint y=0;y<grid.Height;++y) {
            for(uint x=0;x<grid.Width;++x) {
                var tile = grid[y, x];
                var ore = tile.Ore;
                ore.OreType = oreFactory.GetOre(OreType.COPPER);
            }
        }
    }
}
