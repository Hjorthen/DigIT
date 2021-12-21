using System;
using UnityEngine;

public class DestroyOnEnd : IObserver<TileModel> {
    private readonly GameObject obj;

    public DestroyOnEnd(GameObject obj) {
        this.obj = obj;
    }

    public void OnCompleted()
    {
        GameObject.Destroy(obj.gameObject);
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(TileModel value)
    {
    }
}
