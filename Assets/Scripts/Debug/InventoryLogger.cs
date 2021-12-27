using UnityEngine;

public class InventoryLogger {
    public void OnItemAdded(InventoryEntry<Ore> entry) {
        GameConsole.WriteLine(entry.ToString());
    }
}