using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shops/Forge")]
public class ForgeShopHandler : ShopHandler {
    private IPriceProvider priceProvider;

    public override void HandleCommand(string command, GameObject player)
    {
            var inventory = player.GetComponent<OreInventory>().Inventory;
            var currencyStat = player.GetComponent<PlayerStats>().Currency;
            foreach(var inventoryEntry in inventory) {
                var salesData = priceProvider.FromOre(inventoryEntry.item.Type);
                var money = inventoryEntry.quantity * salesData.UnitPrice;
                currencyStat.Currentvalue += money;
                GameConsole.WriteLine($"Sold {inventoryEntry.item.DisplayName} x {inventoryEntry.quantity} for {salesData.UnitPrice * inventoryEntry.quantity}$");
            }
            inventory.Clear();
            GameConsole.WriteLine("Boom. Inventory Sold.");
    }

    public override void Init() {
        priceProvider = ServiceRegistry.GetService<IPriceProvider>();
    }
}
