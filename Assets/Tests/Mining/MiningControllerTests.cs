using NUnit.Framework;

public class MiningTargetControllerTests 
{
    private TickedCooldownTimer timer;
    private MiningController controller;
    private MockMiningTarget miningTarget1;
    private MockMiningTarget miningTarget2;
    private MockMiningListener eventListener;
    private float miningTickDelay = 1.0f;
    private float kMiningStrength = 1.0f;

    [SetUp]
    public void Setup() 
    {
        timer = new TickedCooldownTimer();
        eventListener = new MockMiningListener();
        controller = new MiningController(null, timer, miningTickDelay);
        controller.RegisterListener(eventListener);

        miningTarget1 = new MockMiningTarget();
        miningTarget2 = new MockMiningTarget();
    }

    [Test]
    public void SettingNewMiningTarget_CallsMiningStarted()
    {
        controller.MineTarget(miningTarget1, kMiningStrength);

        Assert.IsTrue(miningTarget1.MiningStartedCalled);
        Assert.IsFalse(miningTarget1.MiningStoppedCalled);
        Assert.IsTrue(eventListener.MiningStartedCalled);
        Assert.IsFalse(eventListener.MiningStoppedCalled);
    }

    [Test]
    public void CallingMineTarget_OnExistingTarget_InvokesMineOnTarget() 
    {
        controller.MineTarget(miningTarget1, kMiningStrength);

        controller.MineTarget(miningTarget1, kMiningStrength);
        Assert.AreEqual(1, miningTarget1.MineCount);
    }

    [Test]
    public void CallingMineTarget_OnExistingTarget_InvokesMineOnTarget_AfterDelay() 
    {
        controller.MineTarget(miningTarget1, kMiningStrength);
        controller.MineTarget(miningTarget1, kMiningStrength);

        // We should still be on cooldown, hence no mining should accour
        controller.MineTarget(miningTarget1, kMiningStrength);
        Assert.AreEqual(1, miningTarget1.MineCount);

        // Advance time to expire cooldown
        timer.AdvanceBy(miningTickDelay);
        controller.MineTarget(miningTarget1, kMiningStrength);

        // We should now have mined again
        Assert.AreEqual(2, miningTarget1.MineCount);
    }

    [Test]
    public void CallingMineTarget_WhenTargetCanNoLongerBeMined_StopsMining()
    {
        controller.MineTarget(miningTarget1, kMiningStrength);

        miningTarget1.canBeMined = false;
        controller.MineTarget(miningTarget1, kMiningStrength);

        Assert.IsTrue(miningTarget1.MiningStoppedCalled);
        Assert.IsTrue(eventListener.MiningStoppedCalled);
    }

    [Test]
    public void CallingStopMining_CallsMiningStopped()
    {
        controller.MineTarget(miningTarget1, kMiningStrength);
        controller.StopMining();

        Assert.IsTrue(miningTarget1.MiningStoppedCalled);
        Assert.IsTrue(eventListener.MiningStoppedCalled);
    }

    [Test]
    public void ChangingMiningTarget_CallsMiningStoppedOnOldTarget_AndMiningStartedOnNewTarget()
    {
        controller.MineTarget(miningTarget1, kMiningStrength);
        controller.MineTarget(miningTarget2, kMiningStrength);

        Assert.IsTrue(miningTarget1.MiningStoppedCalled);
        Assert.IsTrue(miningTarget2.MiningStartedCalled);
        Assert.IsTrue(eventListener.MiningStoppedCalled);
    }

    [Test]
    public void CallingMineTarget_WhenTargetCannotBeMined_DoesNothing()
    {
        miningTarget1.canBeMined = false;

        controller.MineTarget(miningTarget1, kMiningStrength);

        Assert.IsFalse(miningTarget1.MiningStartedCalled);
        Assert.IsFalse(eventListener.MiningStartedCalled);
    }
}
