using UnityEngine;

public class RefuelShopController : MonoBehaviour
{
    [SerializeField]
    private float UnitPrice;
    [SerializeField]
    private RefuelView View;
    [SerializeField]
    private bool Repair;

    void Start() {
        View.OnRefuelClicked += Refuel;
    }

    void OnEnable() {
        var player = GameObject.FindGameObjectWithTag("Player");
        var stats = player.GetComponent<PlayerStats>();
        View.SetFuelPrice(Mathf.CeilToInt(UnitPrice));
        View.SetModel(stats.Fuel);
    }

    void Refuel() {
        var player = GameObject.FindGameObjectWithTag("Player");
        var stats = player.GetComponent<PlayerStats>();
        var stat = Repair ? stats.Hull : stats.Fuel;
        Refuel(stat, stats.Currency);
    }

    public void Refuel(ConsumableStat fuel, ConsumableStat balance) {
        var result = new RefuelingHandler().Refuel(fuel, balance, UnitPrice);
        if(Repair) {
            GameConsole.WriteLine($"Repaired {result.Amount} hull for {result.TotalPrice}");
        } 
        else {
            GameConsole.WriteLine($"Refueled {result.Amount} fuel for {result.TotalPrice}$");
        }
    }
}

public class RefuelingHandler {
    public enum Status {
        SUCCESS,
        INSUFFICIENT_FUNDS
    }
    public struct RefuelResult {
        public float Amount;
        public float TotalPrice;
        public Status Result;
    }

    public RefuelResult Refuel(ConsumableStat fuel, ConsumableStat balance, float unitPrice) {
        var affordableAmount = Mathf.Floor(balance.Currentvalue / unitPrice);
        
        var refuelingAmount = Mathf.Min(fuel.AvailableCapacity(), affordableAmount);
        var price = refuelingAmount * unitPrice;

        if(balance.Withdraw(price)) {
            fuel.Currentvalue += refuelingAmount;
            return new RefuelResult { Amount = refuelingAmount, TotalPrice = price, Result = Status.SUCCESS}; 
        } 

        return new RefuelResult { Amount = 0, TotalPrice = 0, Result = Status.INSUFFICIENT_FUNDS}; 
    }
}