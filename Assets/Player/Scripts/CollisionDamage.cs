using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionDamage : MonoBehaviour
{
    public float VelocityThreshold;
    public int Damage;

    [SerializeField]
    private ConsumableStat Hull;

    private void OnCollisionEnter2D(Collision2D other) {
        var impactVelocity = other.relativeVelocity.y;
        if(impactVelocity >= VelocityThreshold && other.GetContact(0).normal.y > 0.1)
        {
            Debug.Log(impactVelocity);
            Hull.Currentvalue -= Damage;
        }
    }
}
