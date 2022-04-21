using UnityEngine;

public class TileController : MonoBehaviour, MiningTarget
{
    private TileModelBehaviour modelBehaviour;
    public TileModel Model => modelBehaviour.Model;

    public void Start() {
        // We need to get the model through modelBehaviour in case the underlying model changes elsewhere. 
        modelBehaviour = GetComponent<TileModelBehaviour>();
    }

    public bool CanBeMined()
    {
        return !Model.IsDead;
    }

    public void MiningStarted(IMiner miner)
    {
        Model.MinedBy = miner;
    }

    public void MiningStopped(IMiner miner)
    {
        Model.MinedBy = null;
    }

    public void Mine(float progress)
    {
        Model.CurrentLife -= progress;
    }
}
