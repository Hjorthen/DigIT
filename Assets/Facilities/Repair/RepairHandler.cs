using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Shops/Repair")]
public class UpgradeFacility : ShopHandler
{
    [SerializeField]
    private float unitPrice;
    public override void HandleCommand(string command, GameObject player)
    {
        var stats = player.GetComponent<PlayerStats>();

        var balance = stats.Currency; 
        var fuel = stats.Hull;

        var refuelingAmount = fuel.AvailableCapacity();
        var price = refuelingAmount * unitPrice;
        
        balance.Currentvalue -= price;
        fuel.Currentvalue += refuelingAmount;
    }

    public override void Init()
    {
    }
}
