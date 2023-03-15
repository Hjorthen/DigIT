using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public ConsumableStat Currency;
    [SerializeField]
    public ConsumableStat Fuel;
    [SerializeField]
    public ConsumableStat Hull;

    public void OnEnable() {
        StartCoroutine(DrainFuel());
    }

    private IEnumerator DrainFuel() {
        do {
            yield return new WaitForSeconds(1);
            if(Time.timeScale > 0.0f)
                Fuel.Currentvalue = Mathf.Max(0f, Fuel.Currentvalue - 1);
        } while(gameObject.activeInHierarchy);
    }
}
