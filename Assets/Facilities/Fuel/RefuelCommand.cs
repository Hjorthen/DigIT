using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shops/Refuel")]
[System.Serializable]
public class RefuelCommand : ShopHandler
{
    public float UnitPrice;
    public override void HandleCommand(string command, GameObject player)
    {
        var stats = player.GetComponent<PlayerStats>();

        var balance = stats.Currency; 
        var fuel = stats.Fuel;
        Refuel(fuel, balance);
    }

    public void Refuel(ConsumableStat fuel, ConsumableStat balance) {
        var affordableAmount = Mathf.Floor(balance.Currentvalue / UnitPrice);
        
        var refuelingAmount = Mathf.Min(fuel.AvailableCapacity(), affordableAmount);
        var price = refuelingAmount * UnitPrice;

        if(balance.Withdraw(price)) {
            fuel.Currentvalue += refuelingAmount;
        } 
        GameConsole.WriteLine($"Refueled {refuelingAmount} fuel for {price}$");
    }

    public override void Init()
    {
    }
}
