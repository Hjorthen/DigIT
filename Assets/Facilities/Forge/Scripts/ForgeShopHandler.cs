using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shops/Forge")]
public class ForgeShopHandler : ShopHandler {
    private IPriceProvider priceProvider;

    [SerializeField]
    private TransactionLogController transactionLog;

    public override void HandleCommand(string command, GameObject player)
    {
            var inventory = player.GetComponent<OreInventory>().Inventory;
            var currencyStat = player.GetComponent<PlayerStats>().Currency;
            foreach(var inventoryEntry in inventory) {
                var salesData = priceProvider.FromOre(inventoryEntry.item.Type);
                var money = inventoryEntry.quantity * salesData.UnitPrice;
                currencyStat.Currentvalue += money;
                transactionLog.AddEntry($"+ {salesData.UnitPrice * inventoryEntry.quantity}$ ({inventoryEntry.quantity} x {inventoryEntry.item.DisplayName})");
            }
            inventory.Clear();
    }

    public override void Init() {
        priceProvider = ServiceRegistry.GetService<IPriceProvider>();
    }
}
