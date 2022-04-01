using UnityEngine;

public class PlayerEquipment : MonoBehaviour {
    public Drill Drill;

    public Tank Tank;
    public void Start() {
        Drill.AttachTo(this);
        Tank.AttachTo(this);
    }
}