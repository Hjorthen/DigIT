using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRegistry : MonoBehaviour
{
    private Dictionary<Vector2Int, GameObject> registeredTiles;
    public void SetTile(int x, int y, GameObject obj) {
        registeredTiles.Add(new Vector2Int(x, y), obj);
    }

    public GameObject GetTile(int x, int y) {
        if(registeredTiles.TryGetValue(new Vector2Int(x, y), out GameObject value)){
            return value;
        }
        return null;
    }

    void OnEnable()
    {
        registeredTiles = new Dictionary<Vector2Int, GameObject>();
        ServiceRegistry.RegisterService(this);   
    }
}
