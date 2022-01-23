public class MockMiningListener : MiningListener
{
    public bool MiningStoppedCalled = false;
    public bool MiningStartedCalled = false;

    public void OnMiningStopped(IMiner miner)
    {
        MiningStoppedCalled = true;
    }

    public void OnStartMining(IMiner miner)
    {
        MiningStartedCalled = true;
    }
}
