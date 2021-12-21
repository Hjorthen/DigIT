using UnityEngine;

public class TileController : MonoBehaviour, MiningTarget
{
    private TileModelBehaviour modelBehaviour;
    private TileModel model => modelBehaviour.Model;

    private float tickDelay;
    private float tickCooldown = 0.1f;

    public void Start() {
        modelBehaviour = GetComponent<TileModelBehaviour>();
    }

    public void Update() {
        if(tickDelay > 0.0) {
            tickDelay -= Time.deltaTime;
        }
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
        if(tickDelay <= 0.0) {
            model.CurrentLife -= 1;
            tickDelay = tickCooldown;           
        }
    }

    public void OnMiningStopped(IMiner miner)
    {
        model.MinedBy = null;
    }
}
