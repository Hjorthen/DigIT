using UnityEngine;

public class InventoryLogger {
    private readonly TransactionLogController log;

    public InventoryLogger(TransactionLogController log) {
        this.log = log;
    }

    public void OnItemAdded(InventoryEntry<IOre> entry) {
        log.AddEntry(Format(entry));
    }

    private string Format(InventoryEntry<IOre> entry) {
        return $"+{entry.quantity} {entry.item}";
    }
}