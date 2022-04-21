using System;
using UnityEngine;

[System.Serializable]
public class DrillEffect : MonoBehaviour, MiningListener, IObserver<TileModel>
{
    private IDisposable subscriptionCancellationToken;
    [SerializeField]
    private Transform realPlayerPosition;
    [SerializeField]
    private Transform graphicalPlayerOffsetPosition;
    [SerializeField]
    private float DrillDepthRatio;
    private Transform targetPosition;

    public void OnCompleted()
    {
        subscriptionCancellationToken?.Dispose();
        realPlayerPosition.position = graphicalPlayerOffsetPosition.position;
        ResetGraphicalOffset();
    }

    public void OnError(Exception error)
    {
        subscriptionCancellationToken?.Dispose();
        ResetGraphicalOffset();
    }

    public void OnMiningStopped(IMiner miner)
    {
        subscriptionCancellationToken?.Dispose();
        ResetGraphicalOffset();
    }

    public void OnNext(TileModel value)
    {
        float miningProcess = 1 - (value.CurrentLife / value.MaxLife);
        graphicalPlayerOffsetPosition.position = Vector3.Lerp(realPlayerPosition.position, targetPosition.position, miningProcess * DrillDepthRatio);
    }

    private void ResetGraphicalOffset() {
        graphicalPlayerOffsetPosition.localPosition = Vector3.zero;
    }

    public void OnStartMining(IMiner miner)
    {
        if(miner is MinerController minerController) {
            MiningTarget target = miner.CurrentTarget;
            if(target is TileController tile) {
                var model = tile.Model;
                subscriptionCancellationToken = model.Subscribe(this);
                targetPosition = tile.transform;
            }
        }
    }
}
