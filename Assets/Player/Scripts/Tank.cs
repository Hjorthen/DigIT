using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Tank")]
[System.Serializable]
public class Tank : PlayerUpgradeObject
{
    public override PlayerUpgradeType Type => PlayerUpgradeType.TANK;
    public float MaxFuel;

    public override bool AttachTo(PlayerEquipment equipment)
    {
        equipment.Tank = this;
        var stats = equipment.GetComponent<PlayerStats>();
        stats.Fuel.MaxValue = MaxFuel;
        return true;
    }

    public override int CompareTo(PlayerUpgrade other) {
        if(!(other is Tank)) {
            return 0;
        }
        
        return (int)(this.MaxFuel - (other as Tank).MaxFuel);
    }
}
