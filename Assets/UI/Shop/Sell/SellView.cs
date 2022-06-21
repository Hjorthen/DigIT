using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellView : MonoBehaviour
{
    [SerializeField]
    private TransactionLogController transactionLog;
    private IPriceProvider priceProvider;
    void Start()
    {
        priceProvider = ServiceRegistry.GetService<IPriceProvider>();
    }

    public void SellAll()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var inventory = player.GetComponent<OreInventory>().Inventory;
        var currencyStat = player.GetComponent<PlayerStats>().Currency;
        foreach(var inventoryEntry in inventory) {
            var salesData = priceProvider.FromOre(inventoryEntry.item.Type);
            var money = inventoryEntry.quantity * salesData.UnitPrice;
            currencyStat.Currentvalue += money;
            transactionLog.AddEntry($"+{salesData.UnitPrice * inventoryEntry.quantity}$ ({inventoryEntry.quantity} x {inventoryEntry.item.DisplayName})");
        }
        inventory.Clear();
        GameConsole.WriteLine("Boom. Inventory Sold.");
    }
}
