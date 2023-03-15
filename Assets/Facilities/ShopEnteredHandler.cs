using UnityEngine;


[System.Serializable]
public class ShopEnteredHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject shopView;

    public void OnTriggerEnter2D(Collider2D collider) {
        var availableShopHandler = collider.GetComponent<AvailableShopHandler>();
        if(availableShopHandler != null) {
            availableShopHandler.SetAvailableShop(shopView);
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        var availableShopHandler = collider.GetComponent<AvailableShopHandler>();
        if(availableShopHandler != null) {
            availableShopHandler.SetAvailableShop(null);
        }
    }
}