using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class PlayerUpgradeObject : ScriptableObject, PlayerUpgrade {
    public abstract PlayerUpgradeType Type {
        get;
    }

    [field: SerializeField]
    public string Name {
        get; set;
    } 

    [field: SerializeField]
    public int BasePrice {
        get; set;
    }

    public abstract bool AttachTo(PlayerEquipment equipment);


    public abstract int CompareTo(PlayerUpgrade other);
}

[CreateAssetMenu(menuName = "Equipment/Drill")]
[System.Serializable]
public class Drill : PlayerUpgradeObject {
    public float Effeciency;

    public override PlayerUpgradeType Type => PlayerUpgradeType.DRILL;

    public override bool AttachTo(PlayerEquipment equipment)
    {
        equipment.Drill = this;
        return true;
    }

    public override int CompareTo(PlayerUpgrade other) {
        if(!(other is Drill)) {
            return 0;
        }
        
        return (int)(this.Effeciency - (other as Drill).Effeciency);
    }
}

[System.Serializable]
public enum PlayerUpgradeType {
    DRILL,
    TANK
}

public interface PlayerUpgrade : IComparable<PlayerUpgrade> {
    PlayerUpgradeType Type {get;}
    string Name {get;}
    int BasePrice {get;}

    bool AttachTo(PlayerEquipment equipment);
}