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
    // Start is called before the first frame update
    void Start()
    {
        view.Display(RunForemanDialogue());        
    }

    private IEnumerator<DialogueEntry> RunForemanDialogue() {
        string actor = "FOREMAN";

        yield return new DialogueEntry(actor, "Howdy there! Welcome to Mars.\nLooks like the suits gave you just enough fuel to be able to land. Figures. Here, take some credits from me and go get some more.");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Currency.Currentvalue += 600;
        yield return new DialogueEntry(actor, "Good luck!");
    }
}
