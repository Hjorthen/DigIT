using UnityEngine;
using static InteractionEntity;


[System.Serializable]
public class ShopEnteredHandler : MonoBehaviour, InteractionEntity
{
    [SerializeField]
    private GameObject shopView;

    bool InteractionEntity.Active { set { 
        shopView.SetActive(value);
        if(shopView.activeInHierarchy) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }}

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
}