using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiningControllerBase : MonoBehaviour
{
    public float MiningReach = 2;
    private Tile currentMiningTarget;

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
            var tile = hit.collider.GetComponent<Tile>();
            var listener = tile as MiningListener;
            if(tile != null) {
                if(currentMiningTarget != tile) {
                    currentMiningTarget?.OnMiningStopped(tile);
                    Listeners.ForEach(l => l.OnMiningStopped(tile));
                    currentMiningTarget = tile;
                    currentMiningTarget.OnStartMining(tile);
                    Listeners.ForEach(l => l.OnStartMining(tile));
                }
                currentMiningTarget.OnMiningTick(tile);
                Listeners.ForEach(l => l.OnMiningTick(tile));
                if(tile.IsDead) {
                    Listeners.ForEach(l => l.OnMiningStopped(currentMiningTarget));
                    currentMiningTarget = null;
                }
            }
        } else {
            if(currentMiningTarget != null) {
                currentMiningTarget.OnMiningStopped(currentMiningTarget);
                Listeners.ForEach(l => l.OnMiningStopped(currentMiningTarget));
                currentMiningTarget = null;
            }
        }   
    }
}
