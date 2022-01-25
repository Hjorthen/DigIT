using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Shops/Repair")]
public class UpgradeFacility : ShopHandler
{
    [SerializeField]
    private float unitPrice;
    // TODO: Forge this with refuel command, only differing target 
    public override void HandleCommand(string command, GameObject player)
    {
        var stats = player.GetComponent<PlayerStats>();

        var balance = stats.Currency; 
        var hull = stats.Hull;

        var refuelingAmount = hull.AvailableCapacity();
        var price = refuelingAmount * unitPrice;
        
        if(balance.Withdraw(price)) {
            hull.Currentvalue += refuelingAmount;
        }
    }

    public override void Init()
    {
    }
}
