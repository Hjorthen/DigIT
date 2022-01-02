using UnityEngine;

public class InventoryLogger {
    public void OnItemAdded(InventoryEntry<IOre> entry) {
        GameConsole.WriteLine(entry.ToString());
    }
}