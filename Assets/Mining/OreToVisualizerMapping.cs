using System.Collections.Generic;
using UnityEngine;


public interface OreToVisualizerMapper {
    GameObject GetPrefabFor(Ore ore);
}

[CreateAssetMenu]
[System.Serializable]
public class OreToVisualizerMapping : ScriptableObject, OreToVisualizerMapper
{
    [SerializeField]
    private List<string> oreNames;
    [SerializeField]
    private List<GameObject> associatedPrefab;

    public GameObject GetPrefabFor(Ore ore)
    {
        var oreName = ore.Name;
        int index = oreNames.IndexOf(oreName);
        return associatedPrefab[index];
    }
}
