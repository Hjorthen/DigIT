using System;
using UnityEngine;

public interface MiningTarget {
    bool CanBeMined();
    void Mine(float progress);
    void MiningStarted(IMiner miner);
    void MiningStopped(IMiner miner);
}

[Serializable]
public class TileModelBehaviour : MonoBehaviour {
    public TileModel Model;
    private OreToVisualizerMapper OreViewMapper;
    public void Start() {
        var world = ServiceRegistry.GetService<World>();
        var tiles = ServiceRegistry.GetService<TileRegistry>();
        OreViewMapper = ServiceRegistry.GetService<OreToVisualizerMapper>();
        Model = world.GetTileAt(transform.position);
        Model.Subscribe(new DestroyOnEnd(this.gameObject));
        Model.Subscribe(new LootProvider());
        var oreView = new OreView(gameObject, OreViewMapper);
        Model.Subscribe(oreView);
        oreView.DisplayTileAs(Model);

        var gridPosition = world.WorldToGridPosition(transform.position);
        tiles.SetTile(gridPosition.x, gridPosition.y, gameObject);
    }
}

public class OreView : IObserver<TileModel>
{
    private readonly GameObject gameObject;
    private readonly OreToVisualizerMapper oreMapper;
    private Ore shownOre;

    public OreView(GameObject gameObject, OreToVisualizerMapper OreMapper) {
        this.gameObject = gameObject;
        oreMapper = OreMapper;
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(TileModel tile)
    {
        DisplayTileAs(tile);
    }
    public void DisplayTileAs(TileModel tile) 
    {
        var ore = tile.Ore.OreType;
        var prefab = oreMapper.GetPrefabFor(ore);
        RemoveExistingVisualizer();
        AddVisualizer(prefab);
    }

    private void AddVisualizer(GameObject prefab)
    {
        var newVisualizer = GameObject.Instantiate(prefab);
        newVisualizer.name = "Visualizer";
        newVisualizer.transform.SetParent(gameObject.transform);
        newVisualizer.transform.localPosition = Vector3.zero;
    }

    private void RemoveExistingVisualizer()
    {
        var visualizer = gameObject.transform.Find("Visualizer");
        if(visualizer != null)
            GameObject.Destroy(visualizer.gameObject);
    }
}

public class MiningEffect : IObserver<TileModel>
{
    private Material material;
    public MiningEffect(MeshRenderer renderer) {
        material = renderer.material;
    }
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(TileModel value)
    {
        var color = material.color;
        color.r = 1 - value.CurrentLife / value.MaxLife;
        material.color = color;
    }
}

public class LootProvider : IObserver<TileModel>
{
    private bool lootGiven;

    public LootProvider() {
        lootGiven = false;
    }

    public void OnCompleted()
    {}

    public void OnError(Exception error)
    {}

    public void OnNext(TileModel value)
    {
        if(value.IsDead && !lootGiven) {
            lootGiven = true;
            var inventory = value.MinedBy.GetInventory();
            inventory.AddItem(value.Ore.OreType, value.Ore.Quantity);
        }
    }
}

public class LogTileChanges : IObserver<TileModel>
{
    public void OnCompleted()
    {
        Debug.Log("OnCompleted");
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(TileModel value)
    {
        Debug.Log($"Model updated {value}");
    }
}
