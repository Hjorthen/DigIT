using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectController : MonoBehaviour, MiningListener
{
    [SerializeField]
    private ParticleSystem effect;

    public void OnMiningStopped(IMiner miner)
    {
        effect.Stop();
    }

    public void OnMiningTick(IMiner miner)
    {}

    public void OnStartMining(IMiner miner)
    {
        effect.Play();
    }
}
