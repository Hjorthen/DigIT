using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMiningController : MiningControllerBase
{
    [SerializeField]
    private List<DustEffectController> AttachedListeners;
    // Start is called before the first frame update
    void Start()
    {
        AttachedListeners.ForEach(a => RegisterListener(a));
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
