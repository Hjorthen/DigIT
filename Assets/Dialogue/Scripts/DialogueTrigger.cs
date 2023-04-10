using System.Collections.Generic;
using UnityEngine;
using static InteractionEntity;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour, InteractionEntity
{
    [SerializeField]
    private Dialogue dialogue;

    InteractionType InteractionEntity.Type => InteractionType.DIALOGUE;

    public void OnTriggerEnter2D(Collider2D collider) {
        var availableShopHandler = collider.GetComponent<AvailableInteractionHandler>();
        if(availableShopHandler != null) {
            availableShopHandler.SetAvailableInteraction(this);
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        var availableShopHandler = collider.GetComponent<AvailableInteractionHandler>();
        if(availableShopHandler != null) {
            availableShopHandler.SetAvailableInteraction(null);
        }
    }

    private void DisplayDialogue() {
        var dialogueController = ServiceRegistry.GetService<DialogueController>();
        dialogueController.Play(new List<DialogueEntry> {new DialogueEntry("NPC", dialogue.GetMessage())}.GetEnumerator());
    }

    void InteractionEntity.Interact()
    {
        DisplayDialogue();
    }

    void InteractionEntity.Stop()
    {
        // Empty - The interaction stops through other means
    }
}
