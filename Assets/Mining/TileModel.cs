using System;
using System.Collections.Generic;
using UnityEngine;

public struct OreYield {
    public IOre OreType;
    public int Quantity;
}

[System.Serializable]
public class TileModel : IObservable<TileModel> {
    public bool IsDead => CurrentLife <= 0.0;
    public bool Claimed = false;
    public IMiner MinedBy;
    [SerializeField]
    private float currentLife;
    public float MaxLife;
    private List<IObserver<TileModel>> observers;
    [SerializeField]
    private OreYield ore;
    public OreYield Ore {
        get => ore;
        set {
            ore = value;
            InvokeUpdated();
        }
    } 

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

    public TileModel(OreYield ore, float startingLife) {
        this.ore = ore;
        this.currentLife = startingLife;
        this.MaxLife = startingLife;
        this.observers = new List<IObserver<TileModel>>();
    }


    private void InvokeCompleted() {
        for(int i=observers.Count - 1;i>=0;--i) {
            observers[i].OnCompleted();
        }
    }

    private void InvokeUpdated() {
        for(int i=observers.Count - 1;i>=0;--i) 
        {
            observers[i].OnNext(this);
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
