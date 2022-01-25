using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shops/Refuel")]
[System.Serializable]
public class RefuelCommand : ShopHandler
{
    [SerializeField]
    private float unitPrice;
    public override void HandleCommand(string command, GameObject player)
    {
        var stats = player.GetComponent<PlayerStats>();

        var balance = stats.Currency; 
        var fuel = stats.Fuel;

        var refuelingAmount = fuel.AvailableCapacity();
        var price = refuelingAmount * unitPrice;

        if(balance.Withdraw(price)) {
            fuel.Currentvalue += refuelingAmount;
        } else {
            refuelingAmount = balance.Currentvalue / unitPrice;
            if(balance.Withdraw(balance.Currentvalue)) {
                fuel.Currentvalue += Mathf.Ceil(refuelingAmount);
            }
        }
    }

    public override void Init()
    {
    }
}
