using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private ShopItemDescriptionView ShopItemDescriptionView;
    [SerializeField]
    private ShopItemListView ShopItemListView;
    [SerializeField]
    private PurchaseWidget BuyButton;
    [SerializeField]
    private List<PlayerUpgradeObject> items;

    void Start()
    {
        foreach(var item in items) {
            ShopItemListView.AddItem(item, this.OnShopListItemSelected);
        }
    }

    private void OnShopListItemSelected(PlayerUpgrade clickedItem) {

        ShopItemDescriptionView.DisplayItem(clickedItem);
        BuyButton.SetItem(clickedItem, this.OnBuyItem);
    }

    private void OnBuyItem(PlayerUpgrade item) {
        Debug.Log($"Bought {item.Name}");
    }
}
