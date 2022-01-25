using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopHandler : ScriptableObject {
    public abstract void HandleCommand(string command, GameObject player);
    public abstract void Init();
}