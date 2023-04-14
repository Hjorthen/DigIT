using UnityEngine;

[CreateAssetMenu(menuName = "POI/PrefabSpawner")]
public class PrefabFeature : ScriptableObject
{
    [SerializeField]
    protected GameObject _Prefab;
    public virtual void SpawnAt(Vector3 worldPosition) {
        GameObject.Instantiate(_Prefab, worldPosition, _Prefab.transform.rotation);
    }
}
