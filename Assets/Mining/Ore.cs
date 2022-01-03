using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Ore : ScriptableObject, IOre, IMinableObject, ISellableItem {
    [SerializeField]
    private string name;
    public string DisplayName => name;

    [SerializeField]
    private int unitPrice;
    public int UnitPrice {
        get;
    }

    [SerializeField]
    private OreType oreType;
    public OreType Type => oreType;

    [SerializeField]
    private GameObject TilePrefab;

    public GameObject GetTilePrefab()
    {
        return TilePrefab;
    }

    public override string ToString()
    {
        return DisplayName;
    }
}

public interface IOre {
    string DisplayName { get; }
    OreType Type { get; }
}

public interface IMinableObject {
    GameObject GetTilePrefab();
}

public interface ISellableItem {
    int UnitPrice { get; }
}