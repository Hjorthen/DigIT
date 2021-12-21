using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface MiningTarget : MiningListener {
    bool CanBeMined();
}

public class TileModelBehaviour : MonoBehaviour {
    [SerializeField]
    public TileModel Model;
    public void Start() { 
        Model = new TileModel() { CurrentLife = 1000.0f };
        Model.Subscribe(new DestroyOnEnd(this.gameObject));
        Model.Subscribe(new LogTileChanges());
    }
}

public class LogTileChanges : IObserver<TileModel>
{
    public void OnCompleted()
    {
        Debug.Log("OnCompleted");
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(TileModel value)
    {
        Debug.Log($"Model updated {value}");
    }
}
