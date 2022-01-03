using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ConsoleView : MonoBehaviour, IObserver<ConsoleModel>
{
    private int LastOffset = -1;
    [SerializeField]
    private RectTransform parent;
    [SerializeField]
    private Text textPrefab;
    [SerializeField]
    private VerticalLayoutGroup layoutGroup;

    private bool dirty = false;

    public void OnCompleted()
    {}

    public void OnError(Exception error)
    {}

    public void OnNext(ConsoleModel value)
    {
        AppendLineEntry(value.LogEntries.Last());
    }

    private void AppendLineEntry(string line) {
        var newEntry = GameObject.Instantiate(textPrefab.gameObject, parent);
        var textComponent = newEntry.GetComponent<Text>();
        textComponent.text = line;
        dirty = true;
    }


    void LateUpdate() {
        if(dirty) {
            // The layout builder does not automatically update when a child is added. This forces it to do so.
            LayoutRebuilder.ForceRebuildLayoutImmediate(parent);
            dirty = false;
        }
    }

    void Start()
    {
        GameConsole.Instance.Data.Subscribe(this);
    }
}
