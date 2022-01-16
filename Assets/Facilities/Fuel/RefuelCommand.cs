using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shops/Refuel")]
[System.Serializable]
public class RefuelCommand : ShopHandler
{
    [SerializeField]
    private float unitPrice;
    public override void HandleCommand(string command, PlayerMiningController player)
    {
        var stats = player.GetComponent<PlayerStats>();

        var balance = stats.Currency; 
        var fuel = stats.Fuel;

        var refuelingAmount = fuel.AvailableCapacity();
        var price = refuelingAmount * unitPrice;
        
        balance.Currentvalue -= price;
        fuel.Currentvalue += refuelingAmount;
    }

    public override void Init()
    {
    }
}
