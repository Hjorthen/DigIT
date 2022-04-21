using UnityEngine;



public interface IMiner
{
    Inventory<IOre> GetInventory();
    MiningTarget CurrentTarget {
        get;
    }
}