using UnityEngine;
using static InteractionEntity;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour, InteractionEntity
{
    [SerializeField]
    private Dialogue dialogue;

    bool InteractionEntity.Active { set { if(value) DisplayDialogue(); } }

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
        GameConsole.WriteLine(dialogue.GetMessage());
    }
}
