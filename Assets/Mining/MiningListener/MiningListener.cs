public interface MiningListener {
    void OnStartMining(IMiner miner);
    void OnMiningStopped(IMiner miner);
}
