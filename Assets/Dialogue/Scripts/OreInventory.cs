using UnityEngine;

public class OreInventory : MonoBehaviour {

    [SerializeField]
    private TransactionLogController logController;

    public Inventory<IOre> Inventory {
        private set;
        get;
    }

    void Awake() {
        Inventory = new Inventory<IOre>();
        Inventory.OnItemAdded = new InventoryLogger(logController).OnItemAdded;
    }
}