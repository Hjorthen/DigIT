using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour, ICommandHandler
{
    private string command = "sell";
    private PlayerMiningController enteredPlayer;

    void Awake()
    {
        ServiceRegistry.RegisterService<ICommandHandler>(this);
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        var playerController = collider.GetComponent<PlayerMiningController>();
        if(playerController != null) {
            GameConsole.WriteLine($"Command \"{command}\" now available");
            enteredPlayer = playerController;
        }
    }

    public bool HandleCommand(string command) {
        if(command == this.command) {
            var inventory = enteredPlayer.GetInventory();
            foreach(var inventoryEntry in inventory) {
                GameConsole.WriteLine($"Sold {inventoryEntry.item.DisplayName} x {inventoryEntry.quantity}");
            }
            inventory.Clear();
            GameConsole.WriteLine("Boom. Inventory Sold.");
        }

        return true;
    }
}
