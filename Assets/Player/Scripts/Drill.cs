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
}

[CreateAssetMenu(menuName = "Equipment/Drill")]
[System.Serializable]
public class Drill : PlayerUpgradeObject {
    public float Cooldown;

    public override PlayerUpgradeType Type => PlayerUpgradeType.DRILL;

    public override bool AttachTo(PlayerEquipment equipment)
    {
        equipment.Drill = this;
        return true;
    }
}

[System.Serializable]
public enum PlayerUpgradeType {
    DRILL
}

public interface PlayerUpgrade {
    PlayerUpgradeType Type {get;}
    string Name {get;}
    int BasePrice {get;}

    bool AttachTo(PlayerEquipment equipment);
}