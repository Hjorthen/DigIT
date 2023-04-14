using UnityEngine;

[CreateAssetMenu(menuName = "POI/DialogueFeatureSpawner")]
public class DialogueFeatureSpawner : PrefabFeature {
    [SerializeField]
    private Dialogue _Dialogue;

    public override void SpawnAt(Vector3 worldPosition)
    {
        var beacon = GameObject.Instantiate(_Prefab, worldPosition, _Prefab.transform.rotation);
        var dialogueTrigger = beacon.GetComponentInChildren<DialogueTrigger>();
        dialogueTrigger.Dialogue = _Dialogue;
    }
}
