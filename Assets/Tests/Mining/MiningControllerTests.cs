using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MiningControllerTests
{
    private float MiningTickDelay = 1;
    private StaticFloatProvider provider;
    private TickedCooldownTimer timer;
    private MiningController controller;
    private MockMiningTarget miningTarget1;
    private MockMiningTarget miningTarget2;
    
    [SetUp]
    public void Setup() 
    {
        timer = new TickedCooldownTimer();
        provider = new StaticFloatProvider(MiningTickDelay);
        controller = new MiningController(null, timer, provider);

        miningTarget1 = new MockMiningTarget();
        miningTarget2 = new MockMiningTarget();
    }

    [Test]
    public void SettingNewMiningTarget_CallsMiningStarted()
    {
        controller.MineTarget(miningTarget1);

        Assert.IsTrue(miningTarget1.MiningStartedCalled);
        Assert.IsFalse(miningTarget1.MiningStoppedCalled);
    }

    [Test]
    public void CallingMineTarget_OnExistingTarget_InvokesMineOnTarget() 
    {
        controller.MineTarget(miningTarget1);

        controller.MineTarget(miningTarget1);
        Assert.AreEqual(1, miningTarget1.MineCount);
    }

    [Test]
    public void CallingMineTarget_OnExistingTarget_InvokesMineOnTarget_AfterDelay() 
    {
        controller.MineTarget(miningTarget1);
        controller.MineTarget(miningTarget1);

        // We should still be on cooldown, hence no mining should accour
        controller.MineTarget(miningTarget1);
        Assert.AreEqual(1, miningTarget1.MineCount);

        // Advance time to expire cooldown
        timer.AdvanceBy(MiningTickDelay);
        controller.MineTarget(miningTarget1);

        // We should now have mined again
        Assert.AreEqual(2, miningTarget1.MineCount);
    }

    [Test]
    public void CallingMineTarget_WhenTargetCanNoLongerBeMined_StopsMining()
    {
        controller.MineTarget(miningTarget1);

        miningTarget1.canBeMined = false;
        controller.MineTarget(miningTarget1);

        Assert.IsTrue(miningTarget1.MiningStoppedCalled);
    }

    [Test]
    public void CallingStopMining_CallsMiningStopped()
    {
        controller.MineTarget(miningTarget1);
        controller.StopMining();

        Assert.IsTrue(miningTarget1.MiningStoppedCalled);
    }

    [Test]
    public void ChangingMiningTarget_CallsMiningStoppedOnOldTarget_AndMiningStartedOnNewTarget()
    {
        controller.MineTarget(miningTarget1);
        controller.MineTarget(miningTarget2);

        Assert.IsTrue(miningTarget1.MiningStoppedCalled);
        Assert.IsTrue(miningTarget2.MiningStartedCalled);
    }

    [Test]
    public void CallingMineTarget_WhenTargetCannotBeMined_DoesNothing()
    {
        miningTarget1.canBeMined = false;

        controller.MineTarget(miningTarget1);

        Assert.IsFalse(miningTarget1.MiningStartedCalled);
    }
}

class MockMiningTarget : MiningTarget
{
    public int MineCount {
        get;
        private set;
    }

    public bool canBeMined = true;
    
    public bool MiningStartedCalled = false;
    public bool MiningStoppedCalled = false;

    public bool CanBeMined() => canBeMined;

    public void Mine(float progress)
    {
        MineCount++;
    }

    public void MiningStarted(IMiner miner)
    {
        MiningStartedCalled = true;
    }

    public void MiningStopped(IMiner miner)
    {
        MiningStoppedCalled = true;
    }
}