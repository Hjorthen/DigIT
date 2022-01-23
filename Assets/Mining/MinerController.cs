using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickedCooldownTimer : ICooldownTimer
{
    private float remainingCooldown;
    public bool Expired => remainingCooldown <= 0;

    public void WaitFor(float seconds)
    {
        remainingCooldown = seconds;
    }

    public void AdvanceBy(float delta) {
        remainingCooldown = Mathf.Max(0.0f, remainingCooldown - delta);
    }
}

public interface ICooldownTimer {
    bool Expired {
        get;
    }

    void WaitFor(float seconds);
}

public interface IFloatProvider {
    public float Value {
        get;
    }
}

public class StaticFloatProvider : IFloatProvider
{
    public StaticFloatProvider(float value) {
        Value = value;
    }
    public float Value {
        private set;
        get;
    }
}

public class MiningController {
    private MiningTarget currentMiningTarget;
    private List<MiningListener> Listeners = new List<MiningListener>();
    private readonly IMiner owner;
    private readonly ICooldownTimer miningCooldown;
    private readonly IFloatProvider miningTickDelay;

    public MiningController(IMiner owner, ICooldownTimer timer, IFloatProvider miningTickDelay) {
        this.owner = owner;
        this.miningCooldown = timer;
        this.miningTickDelay = miningTickDelay;
    }

    public void RegisterListener(MiningListener listener) {
        Listeners.Add(listener);
    }

    public void MineTarget(MiningTarget tile)
    {
        if (currentMiningTarget != tile && tile.CanBeMined())
        {
            SetMiningTarget(tile);
        }
        if (currentMiningTarget != null)
        {
            if (currentMiningTarget.CanBeMined())
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
        if(currentMiningTarget == null)
            return;
        currentMiningTarget.MiningStopped(owner);
        Listeners.ForEach(l => l.OnMiningStopped(owner));
        currentMiningTarget = null;
    }

    private void MineTarget() {
        if(miningCooldown.Expired) {
            miningCooldown.WaitFor(miningTickDelay.Value);
            currentMiningTarget.Mine(1.0f);
        }
    }

    private void SetMiningTarget(MiningTarget tile)
    {
        StopMining();
        StartMining(tile);
    }

    private void StartMining(MiningTarget tile)
    {
        currentMiningTarget = tile;
        currentMiningTarget.MiningStarted(owner);
        Listeners.ForEach(l => l.OnStartMining(owner));
    }
}

public class MinerController : MonoBehaviour, IMiner
{
    public float MiningReach = 2;

    private MiningTarget currentMiningTarget;
    private MiningController miningController;
    private TickedCooldownTimer miningTimer;
    private Inventory<IOre> Inventory;

    public void Start() {
        miningTimer = new TickedCooldownTimer();
        miningController = new MiningController(this, miningTimer, new StaticFloatProvider(0.1f));
        Inventory = new Inventory<IOre>();
        Inventory.OnItemAdded = new InventoryLogger().OnItemAdded;
    }


    [SerializeField]
    private List<MiningListener> Listeners = new List<MiningListener>();

    public void RegisterListener(MiningListener listener) {
        Listeners.Add(listener);
    }

    public void MiningTick(Vector2 direction)
    {
        miningTimer.AdvanceBy(Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, MiningReach, LayerMask.GetMask("MiningLayer"));
        if(hit.collider != null)
        {
            var tile = hit.collider.GetComponent<MiningTarget>();
            if(tile != null)
            {
                miningController.MineTarget(tile);
            }
        } else {
            if(currentMiningTarget != null)
            {
                miningController.StopMining();
            }
        }   
    }

    public Inventory<IOre> GetInventory()
    {
        return Inventory;
    }
}
