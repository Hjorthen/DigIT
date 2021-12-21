using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiningControllerBase : MonoBehaviour, IMiner
{
    public float MiningReach = 2;
    private MiningTarget currentMiningTarget;

    [SerializeField]
    private List<MiningListener> Listeners = new List<MiningListener>();

    public void RegisterListener(MiningListener listener) {
        Listeners.Add(listener);
    }

    public void MiningTick(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, MiningReach, LayerMask.GetMask("MiningLayer"));
        if(hit.collider != null)
        {
            var tile = hit.collider.GetComponent<MiningTarget>();
            var listener = tile as MiningListener;
            if(tile != null) {
                if(currentMiningTarget != tile && tile.CanBeMined())
                {
                    SetMiningTarget(tile);
                }
                if(currentMiningTarget != null) {
                    if(currentMiningTarget.CanBeMined()) {
                        currentMiningTarget.OnMiningTick(this);
                        Listeners.ForEach(l => l.OnMiningTick(this));
                    } else {
                        Listeners.ForEach(l => l.OnMiningStopped(this));
                        currentMiningTarget = null;
                    }
                }
            }
        } else {
            if(currentMiningTarget != null) {
                currentMiningTarget.OnMiningStopped(this);
                Listeners.ForEach(l => l.OnMiningStopped(this));
                currentMiningTarget = null;
            }
        }   
    }

    private void SetMiningTarget(MiningTarget tile)
    {
        currentMiningTarget?.OnMiningStopped(this);
        Listeners.ForEach(l => l.OnMiningStopped(this));
        currentMiningTarget = tile;
        currentMiningTarget.OnStartMining(this);
        Listeners.ForEach(l => l.OnStartMining(this));
    }
}
