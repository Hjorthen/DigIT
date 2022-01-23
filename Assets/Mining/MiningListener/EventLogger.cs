using UnityEngine;

public class EventLogger : MiningListener
{
    public void OnMiningStopped(IMiner miner)
    {
        Debug.Log("Mining stopped");
    }

    public void OnStartMining(IMiner miner)
    {
        Debug.Log("Mining started");
    }
}
