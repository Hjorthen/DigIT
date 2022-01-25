using UnityEngine;

public class OreInventory : MonoBehaviour {
    public Inventory<IOre> Inventory {
        private set;
        get;
    }

    void Awake() {
        Inventory = new Inventory<IOre>();
        Inventory.OnItemAdded = new InventoryLogger().OnItemAdded;
    }
}