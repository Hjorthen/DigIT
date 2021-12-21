using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileMiningEvent : UnityEvent<Tile> {}

public interface MiningListener {
    void OnStartMining(Tile tile);
    void OnMiningTick(Tile tile);
    void OnTileLifeExpired(Tile tile);
    void OnMiningStopped(Tile tile);
}

public class EventLogger : MiningListener
{
    public void OnMiningStopped(Tile tile)
    {
        Debug.Log("Mining stop");
    }

    public void OnMiningTick(Tile tile)
    {
    }

    public void OnStartMining(Tile tile)
    {
        Debug.Log("Mining start");
    }

    public void OnTileLifeExpired(Tile tile)
    {
        Debug.Log("Tile died");
    }
}

public class ShakeItBaby : MiningListener
{
    private Vector3 initialPosition;
    private float sign = 1;
    public void OnMiningStopped(Tile tile)
    {
        tile.transform.position = initialPosition;
    }

    public void OnMiningTick(Tile tile)
    {
        Vector3 offsetPosition = initialPosition;
        offsetPosition.x += 0.05f * sign;
        sign = -sign;
        tile.transform.position = offsetPosition;
    }

    public void OnStartMining(Tile tile)
    {
        initialPosition = tile.transform.position;
    }

    public void OnTileLifeExpired(Tile tile)
    {
    }
}

public class DestroyOnEndMining : MiningListener {
    public void OnDestroyed(Tile tile) {}

    public void OnMiningStopped(Tile tile) {}

    public void OnMiningTick(Tile tile) {}

    public void OnStartMining(Tile tile) {}

    public void OnTileLifeExpired(Tile tile)
    {
        GameObject.Destroy(tile.gameObject);
    }
}

public class TileObject : System.IObservable<TileObject> {
    private ICollection<IObserver<TileObject>> observers;
    private float currentLife;
    private bool isDestroyed = false;
    public float CurrentLife {
        get => currentLife;
        set {
            currentLife += value;
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
    IDisposable IObservable<TileObject>.Subscribe(IObserver<TileObject> observer)
    {
        if(observers == null)
            observers = new List<IObserver<TileObject>>();
        observers.Add(observer);
        return new Unsubscriber<TileObject>(observers, observer);
    }
}

public class Unsubscriber<T> : IDisposable
{
    private ICollection<IObserver<T>> observers;
    private IObserver<T> @this;

    public Unsubscriber(ICollection<IObserver<T>> observers, IObserver<T> @this) {
        this.observers = observers;
        this.@this = @this;
    }

    public void Dispose()
    {
        if(observers != null && @this != null && observers.Contains(@this)) {
            observers.Remove(@this);
        }
        @this = null;
        observers = null;
    }
}

public class Tile : MonoBehaviour, MiningListener
{
    [SerializeField]
    private List<MiningListener> Listeners = new List<MiningListener>();
    private float LifeTime = 2.0f;
    private float CurrentLifeTime = 2.0f;
    public bool IsDead => LifeTime <= 0.0;

    public void Start() {
        Listeners.Add(new ShakeItBaby());
        Listeners.Add(new DestroyOnEndMining());
    }

    public void OnStartMining(Tile tile)
    {
        Listeners.ForEach(a => a.OnStartMining(this));
    }

    public void OnMiningTick(Tile tile)
    {
        LifeTime -= Time.deltaTime;
        Listeners.ForEach(a => a.OnMiningTick(this));
        if(IsDead) {
            Listeners.ForEach(a => a.OnTileLifeExpired(this));
        }
    }

    public void OnMiningStopped(Tile tile)
    {
        Listeners.ForEach(a => a.OnMiningStopped(this));
    }

    public void OnTileLifeExpired(Tile tile)
    {
    }
}
