using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMiningController : MinerController
{
    void Start()
    {
        base.Start();
        System.Array.ForEach(GetComponents<MiningListener>(), a => miningController.RegisterListener(a));
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Mathf.Min(0, Input.GetAxis("Vertical"));

        // Dont allow mining on two axis
        if(Mathf.Abs(vertical) > 0)
            horizontal = 0;
        var PlayerMiningDirection = new Vector2(horizontal, vertical).normalized;
        base.MiningTick(PlayerMiningDirection);
    }
}
