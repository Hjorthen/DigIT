using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectController : MonoBehaviour, MiningListener
{
    [SerializeField]
    private ParticleSystem effect;
    public void OnMiningStopped(Tile tile)
    {
        effect.Stop();
    }

    public void OnMiningTick(Tile tile)
    {}

    public void OnStartMining(Tile tile)
    {
        effect.Play();
    }

    public void OnTileLifeExpired(Tile tile)
    {}

    // Update is called once per frame
    void Update()
    {
        
    }
}
