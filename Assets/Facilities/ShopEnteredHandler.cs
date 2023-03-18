using UnityEngine;
using static InteractionEntity;


[System.Serializable]
public class ShopEnteredHandler : MonoBehaviour, InteractionEntity
{
    [SerializeField]
    private GameObject shopView;

    InteractionType InteractionEntity.Type => InteractionType.SHOP;

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

    void InteractionEntity.Interact()
    {
        shopView.SetActive(true);
        Time.timeScale = 0;
    }

    void InteractionEntity.Stop()
    {
        shopView.SetActive(false);
        Time.timeScale = 1;
    }
}