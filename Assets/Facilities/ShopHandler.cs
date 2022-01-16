using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopHandler : ScriptableObject {
    public abstract void HandleCommand(string command, PlayerMiningController player);
    public abstract void Init();
}