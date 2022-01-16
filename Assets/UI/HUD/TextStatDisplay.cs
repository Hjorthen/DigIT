using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TextStatDisplay : MonoBehaviour, System.IObserver<IConsumableStat<float>>
{
    [SerializeField]
    private ConsumableStat stat;
    [SerializeField]
    private Text text;
    private string formatString;
    private IDisposable subscriber = null;

    void IObserver<IConsumableStat<float>>.OnCompleted()
    { }

    void IObserver<IConsumableStat<float>>.OnError(Exception error)
    { }

    void IObserver<IConsumableStat<float>>.OnNext(IConsumableStat<float> value)
    {
        text.text = String.Format(formatString, value.Currentvalue);
    }

    void Start() {
        formatString = text.text;
        subscriber = stat.Subscribe(this);
    }

    void OnDestroy() {
        subscriber?.Dispose();
    }
}
