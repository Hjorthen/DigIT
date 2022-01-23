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

    public void MiningStarted(IMiner miner)
    {
        model.MinedBy = miner;
    }

    public void MiningStopped(IMiner miner)
    {
        model.MinedBy = null;
    }

    public void Mine(float progress)
    {
        model.CurrentLife -= progress;
    }
}
