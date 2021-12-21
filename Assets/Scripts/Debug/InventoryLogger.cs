using UnityEngine;

public class InventoryLogger {
    public void OnItemAdded(InventoryEntry<Ore> entry) {
        Debug.Log(entry);
    }
}