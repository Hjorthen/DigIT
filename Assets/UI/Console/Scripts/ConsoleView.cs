using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ConsoleView : MonoBehaviour, IObserver<ConsoleModel>
{
    private int LastOffset = -1;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Text textPrefab;

    public void OnCompleted()
    {}

    public void OnError(Exception error)
    {}

    public void OnNext(ConsoleModel value)
    {
        AppendLineEntry(value.LogEntries.Last());
    }

    private void AppendLineEntry(string line) {
        var newEntry = GameObject.Instantiate(textPrefab.gameObject);
        var textComponent = newEntry.GetComponent<Text>();
        textComponent.text = line;
        newEntry.transform.SetParent(parent);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameConsole.Instance.Data.Subscribe(this);
    }
}
