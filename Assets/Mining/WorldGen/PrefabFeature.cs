using UnityEngine;

[CreateAssetMenu(menuName = "POI/PrefabSpawner")]
public class PrefabFeature : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;
    public void SpawnAt(Vector3 worldPosition) {
        GameObject.Instantiate(prefab, worldPosition, prefab.transform.rotation);
    }
}
