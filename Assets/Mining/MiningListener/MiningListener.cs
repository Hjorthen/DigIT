public interface MiningListener {
    void OnStartMining(IMiner miner);
    void OnMiningTick(IMiner miner);
    void OnMiningStopped(IMiner miner);
}
