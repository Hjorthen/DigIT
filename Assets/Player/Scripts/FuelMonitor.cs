using System;
using UnityEngine;

public class FuelMonitor : MonoBehaviour, System.IObserver<IConsumableStat<float>>
{
    [SerializeField]
    private ConsumableStat FuelStat;
    [SerializeField]
    private GameObject lighting;
    [SerializeField]
    private GameObject emergencyLighting;

    [SerializeField]
    private RefuelPresenter RefuelPresenter;

    private IDisposable subscription;
    private bool lowFuelState;

    public void OnNext(IConsumableStat<float> stat)
    {
        if(stat.Currentvalue <= 0) {
            OnNoFuel();
        } else {
            OnFuel();
        }
    }

    private void OnFuel()
    {
        if(lowFuelState) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CanMove = true;
            emergencyLighting.SetActive(false);
            lighting.SetActive(true);
            lowFuelState = false;
            RefuelPresenter.HideRefuelPanel();
        }
    }

    private void OnNoFuel()
    {
        if(!lowFuelState) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CanMove = false;
            emergencyLighting.SetActive(true);
            lighting.SetActive(false);
            lowFuelState = true;
            RefuelPresenter.DisplayRefuelPanel();
        }
    }

    void Start()
    {
        lowFuelState = false;
        subscription = FuelStat.Subscribe(this);
    }

    public void OnCompleted()
    {
        subscription?.Dispose();
    }

    public void OnError(Exception error)
    {
        subscription?.Dispose();
    }
}
