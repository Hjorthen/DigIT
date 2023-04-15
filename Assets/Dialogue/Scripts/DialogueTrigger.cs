using System.Collections.Generic;
using UnityEngine;
using static InteractionEntity;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour, InteractionEntity
{
    public Dialogue Dialogue;

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
        dialogueController.Play(Dialogue.StartDialogue());
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
