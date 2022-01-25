using System;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumableStat<T> : IObservable<IConsumableStat<T>> {
    T MaxValue { get; }
    T Currentvalue { get; set; }
}

static public class IConsumableStatExtensions {
    public static float CurrentRatio(this IConsumableStat<float> stat) {
        return stat.Currentvalue / stat.MaxValue;
    }

    public static float AvailableCapacity(this IConsumableStat<float> stat) {
        return stat.MaxValue - stat.Currentvalue;
    }
}

[System.Serializable]
public class ConsumableStat : MonoBehaviour, IConsumableStat<float>
{
    private ICollection<IObserver<IConsumableStat<float>>> observers = new List<IObserver<IConsumableStat<float>>>();

    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float currentValue;
    [SerializeField]
    private string label;

    public float MaxValue {
        get => maxValue;
        set {
            maxValue = value;
            InvokeChanged();
        }
    }

    public bool Withdraw(float amount) {
        if(currentValue >= amount) {
            currentValue-= amount;
            return true;
        }
        return false;
    }

    public float Currentvalue { 
        get => currentValue; 
        set {
            currentValue = value;
            InvokeChanged();
        } 
    }

    private void InvokeChanged() {
        foreach(var observer in observers) {
            observer.OnNext(this);
        }
    }

    public IDisposable Subscribe(IObserver<IConsumableStat<float>> observer)
    {
        observers.Add(observer);
        observer.OnNext(this);
        return new Unsubscriber<IConsumableStat<float>>(observers, observer);
    }
}