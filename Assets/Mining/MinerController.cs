using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICooldownTimer {
    bool Expired {
        get;
    }

    void WaitFor(float seconds);
}

public class MiningController {
    public MiningTarget CurrentMiningTarget{
        private set;
        get;
    }
    private List<MiningListener> Listeners = new List<MiningListener>();
    private readonly IMiner owner;
    private readonly ICooldownTimer miningCooldown;
    private readonly Func<float> miningTickDelay;

    public MiningController(IMiner owner, ICooldownTimer timer, Func<float> miningTickDelay) {
        this.owner = owner;
        this.miningCooldown = timer;
        this.miningTickDelay = miningTickDelay;
    }

    public void RegisterListener(MiningListener listener) {
        Listeners.Add(listener);
    }

    public void MineTarget(MiningTarget tile)
    {
        if (CurrentMiningTarget != tile && tile.CanBeMined())
        {
            SetMiningTarget(tile);
        }
        if (CurrentMiningTarget != null)
        {
            if (CurrentMiningTarget.CanBeMined())
            {
                MineTarget();
            }
            else
            {
                StopMining();
            }
        }
    }

    public void StopMining()
    {
        if(CurrentMiningTarget == null)
            return;
        CurrentMiningTarget.MiningStopped(owner);
        Listeners.ForEach(l => l.OnMiningStopped(owner));
        CurrentMiningTarget = null;
    }

    private void MineTarget() {
        if(miningCooldown.Expired) {
            miningCooldown.WaitFor(miningTickDelay());
            CurrentMiningTarget.Mine(1.0f);
        }
    }

    private void SetMiningTarget(MiningTarget tile)
    {
        StopMining();
        StartMining(tile);
    }

    private void StartMining(MiningTarget tile)
    {
        CurrentMiningTarget = tile;
        CurrentMiningTarget.MiningStarted(owner);
        Listeners.ForEach(l => l.OnStartMining(owner));
    }
}

public class MinerController : MonoBehaviour, IMiner
{
    public float MiningReach = 2;
    public float BaseMiningTickDelay = 2;

    protected MiningController miningController;
    private TickedCooldownTimer miningTimer;
    private PlayerEquipment equipment;
    private Inventory<IOre> inventory;

    public void Start() {
        miningTimer = new TickedCooldownTimer();
        equipment = GetComponent<PlayerEquipment>();
        miningController = new MiningController(this, miningTimer, () => BaseMiningTickDelay * (1 / equipment.Drill.Effeciency));
        inventory = GetComponent<OreInventory>().Inventory;
    }


    public MiningTarget CurrentTarget => miningController.CurrentMiningTarget;

    public void MiningTick(Vector2 direction)
    {
        miningTimer.AdvanceBy(Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, MiningReach, LayerMask.GetMask("MiningLayer"));
        if(hit.collider != null)
        {
            var tile = hit.collider.GetComponent<MiningTarget>();
            if(tile != null)
            {
                Debug.DrawRay(transform.position, direction.normalized * MiningReach, Color.green);
                miningController.MineTarget(tile);
            }
        } else {
            Debug.DrawRay(transform.position, direction.normalized * MiningReach, Color.red);
            miningController.StopMining();
        }   
    }

    public Inventory<IOre> GetInventory()
    {
        return inventory;
    }
}
