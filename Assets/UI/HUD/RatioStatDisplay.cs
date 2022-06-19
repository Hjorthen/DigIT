using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RatioStatDisplay : MonoBehaviour, System.IObserver<IConsumableStat<float>>
{
    [SerializeField]
    private RectTransform ratio;
    [SerializeField]
    private ConsumableStat stat;
    [SerializeField]
    private Animator lowAlertAnimation;
    private IDisposable subscriber = null;

    void Start() {
        subscriber = stat.Subscribe(this);
    }

    public void OnCompleted()
    {}

    public void OnError(Exception error)
    {}

    public void OnNext(IConsumableStat<float> stat)
    {
        float fillRatio = Mathf.Clamp(stat.CurrentRatio(), 0, 1);
        var anchor = ratio.anchorMax;
        anchor.x = fillRatio;
        ratio.anchorMax = anchor;
        
        if(stat.CurrentRatio() <= 0.2) {
            lowAlertAnimation.Play("Base.Alert");
        } else {
            lowAlertAnimation.Play("Base.Idle");
        }
    }

    void OnDestroy() {
        subscriber?.Dispose();
    }    

}
