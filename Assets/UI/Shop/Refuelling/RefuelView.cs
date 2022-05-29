using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RefuelView : MonoBehaviour, IObserver<IConsumableStat<float>>
{
    [SerializeField]
    private Text PriceLabel;
    [SerializeField]
    private Text capacityLabel;
    [SerializeField]
    private Button RefuelTrigger;
    public UnityAction OnRefuelClicked;

    private ConsumableStat model;        
    private IDisposable listeningToken;

    void Awake() {
        RefuelTrigger.onClick.AddListener(TriggerRefuel);
    }

    public void SetFuelPrice(int price) {
        PriceLabel.text = $"{price}$";
    }

    public void SetModel(ConsumableStat model) {
        this.model = model;
        model.Subscribe(this);
    }

    private void TriggerRefuel() {
        if(OnRefuelClicked != null)
            OnRefuelClicked();
    }

    private void StopWatchingModel() {
        if(listeningToken != null) {
            listeningToken.Dispose();
            listeningToken = null;
        }
        model = null;
    }

    private void UpdateCapacityLabel() {
        capacityLabel.text = $"{model.Currentvalue} / {model.MaxValue}";
    }

    void IObserver<IConsumableStat<float>>.OnCompleted()
    {
        throw new System.NotImplementedException();
    }

    void IObserver<IConsumableStat<float>>.OnError(Exception error)
    {
        throw new System.NotImplementedException();
    }

    void IObserver<IConsumableStat<float>>.OnNext(IConsumableStat<float> value)
    {
        UpdateCapacityLabel();
    }
}
