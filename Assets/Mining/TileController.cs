using UnityEngine;

public class TileController : MonoBehaviour, MiningTarget
{
    private TileModelBehaviour modelBehaviour;
    private TileModel model => modelBehaviour.Model;
    public void Start() {
        modelBehaviour = GetComponent<TileModelBehaviour>();
    }

    public bool CanBeMined()
    {
        return !model.IsDead;
    }

    public void OnStartMining(IMiner miner)
    {
        model.MinedBy = miner;
    }

    public void OnMiningTick(IMiner miner)
    {
        model.CurrentLife -= 1;
    }

    public void OnMiningStopped(IMiner miner)
    {
        model.MinedBy = null;
    }
}
