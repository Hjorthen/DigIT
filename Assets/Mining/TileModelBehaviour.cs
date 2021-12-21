using System;
using UnityEngine;

public interface MiningTarget : MiningListener {
    bool CanBeMined();
}

public class TileModelBehaviour : MonoBehaviour {
    [SerializeField]
    public TileModel Model;
    public void Start() {
        Model = new TileModel(new OreYield {OreType = new Ore(), Quantity = 5}, 10); 
        Model.Subscribe(new DestroyOnEnd(this.gameObject));
        Model.Subscribe(new LootProvider());
        Model.Subscribe(new MiningEffect(this.GetComponent<MeshRenderer>()));
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
