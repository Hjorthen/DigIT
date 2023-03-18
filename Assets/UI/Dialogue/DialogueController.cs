using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DialogueEntry {
    public string actorName;
    public string text;

    public DialogueEntry(string actorName, string text) : this()
    {
        this.actorName = actorName;
        this.text = text;
    }
}

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private DialogueView view;

    void Awake() {
        ServiceRegistry.RegisterService<DialogueController>(this);
    }

    public void Play(IEnumerator<DialogueEntry> dialogue) {
        gameObject.SetActive(this);
        view.Display(dialogue);
    }

}
