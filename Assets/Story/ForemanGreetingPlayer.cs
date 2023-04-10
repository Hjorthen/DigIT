using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForemanGreetingPlayer : MonoBehaviour
{
    void Start()
    {
        var dialogueController = ServiceRegistry.GetService<DialogueController>();
        dialogueController.Play(CreateForemanDialogue());
    }

    private IEnumerator<DialogueEntry> CreateForemanDialogue() {
        string actor = "FOREMAN";

        yield return new DialogueEntry(actor, "Howdy there! Welcome to Mars! I'm the foreman at this site.\nLooks like the suits gave you just enough fuel to be able to land. Figures. Here, take some credits from me and go get some more.");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Currency.Currentvalue += 50 * 7;
        yield return new DialogueEntry(actor, "Good luck!");
    }
}
