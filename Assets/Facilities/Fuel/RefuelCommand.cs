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
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
    }
}
