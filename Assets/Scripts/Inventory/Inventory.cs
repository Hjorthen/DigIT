using System.Collections.Generic;
using System.Linq;

public struct InventoryEntry<T> {
    public InventoryEntry(T item, int quantity) {
        this.item = item;
        this.quantity = quantity;
    }
    public readonly T item;
    public int quantity;

    public override string ToString()
    {
        return $"InventoryEntry: {item} x {quantity}";
    }
}

public class Inventory<T> {
    public delegate void InventoryEvent(InventoryEntry<T> item);
    private List<InventoryEntry<T>> collection = new List<InventoryEntry<T>>();
    public InventoryEvent OnItemAdded;

    public void AddItem(T item, int quantity = 1) {
        var existingIndex = collection.FindIndex(e => EqualityComparer<T>.Default.Equals(item));
        InventoryEntry<T> entry = new InventoryEntry<T>(item, quantity);
        if(existingIndex != -1) {
            entry = collection[existingIndex];
            entry.quantity += quantity;
            collection[existingIndex] = entry;
        } else {
            collection.Add(entry);
        }
        OnItemAdded(entry);
    }
}