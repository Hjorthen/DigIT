using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeathMonitor : MonoBehaviour, System.IObserver<IConsumableStat<float>>
{
    [SerializeField]
    private List<ConsumableStat> monitoredStats;
    [SerializeField]
    private GameObject deathOverlay;

    [SerializeField]
    private GameObject player;

    public void OnNext(IConsumableStat<float> value)
    {
        if(value.Currentvalue <= 0) {
            deathOverlay.SetActive(true);
            player.SetActive(false);
        }
    }

    void Start()
    {
        foreach(var stat in monitoredStats) {
            stat.Subscribe(this);
        }
    }

    public void OnCompleted()
    {}

    public void OnError(Exception error)
    {}
}
