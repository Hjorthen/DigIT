using System.Collections.Generic;
using UnityEngine;


public interface OreToVisualizerMapper {
    GameObject GetPrefabFor(IOre ore);
}

[CreateAssetMenu]
[System.Serializable]
public class OreToVisualizerMapping : ScriptableObject, OreToVisualizerMapper
{
    [SerializeField]
    private List<string> oreNames;
    [SerializeField]
    private List<GameObject> associatedPrefab;

    public GameObject GetPrefabFor(IOre ore)
    {
        var oreName = ore.DisplayName;
        int index = oreNames.IndexOf(oreName);
        return associatedPrefab[index];
    }
}
