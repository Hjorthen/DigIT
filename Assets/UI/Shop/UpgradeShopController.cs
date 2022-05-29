using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopController : MonoBehaviour
{
    [SerializeField]
    private ShopItemDescriptionView ShopItemDescriptionView;
    [SerializeField]
    private ShopItemListView ShopItemListView;
    [SerializeField]
    private PurchaseWidget BuyButton;
    [SerializeField]
    private List<PlayerUpgradeObject> items;
    [SerializeField]
    private PopupDialog PopUp;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var playerCurrency = player.GetComponent<PlayerStats>().Currency;
        var playerEquipment = player.GetComponent<PlayerEquipment>();
    
        if(playerCurrency.Withdraw(item.BasePrice)) {
            item.AttachTo(playerEquipment);
            PopUp.Show("Upgrade " + item.Name + " bought!");
        } else {
            PopUp.Show("Insufficient Funds");
        }
    }
}
