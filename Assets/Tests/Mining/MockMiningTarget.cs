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