using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileModel : IObservable<TileModel> {
    internal IMiner MinedBy;
    private ICollection<IObserver<TileModel>> observers = new List<IObserver<TileModel>>();
    [SerializeField]
    private float currentLife;
    private bool isDestroyed = false;
    public float CurrentLife {
        get => currentLife;
        set {
            currentLife = value;
            if(!isDestroyed)
                InvokeUpdated();

            if(this.IsDead && !isDestroyed) {
                isDestroyed = true;
                InvokeCompleted();
            }
        }
    }

    public bool IsDead => CurrentLife <= 0.0;

    private void InvokeCompleted() {
        foreach(var observer in observers) {
            observer.OnCompleted();
        }
    }

    private void InvokeUpdated() {
        foreach (var observer in observers)
        {
            observer.OnNext(this);
        }
    }
    public IDisposable Subscribe(IObserver<TileModel> observer)
    {
        if(observers == null)
            observers = new List<IObserver<TileModel>>();
        observers.Add(observer);
        return new Unsubscriber<TileModel>(observers, observer);
    }

    public override string ToString()
    {
        return $"Tile {currentLife} health";
    }
}
