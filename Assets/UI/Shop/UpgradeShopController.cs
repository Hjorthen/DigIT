using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Button FuelTanksButton;
    [SerializeField]
    private Button HullButton;
    [SerializeField]
    private Button EngineButton;
    [SerializeField]
    private Button DrillButton;


    void Start()
    {
        DisplayItemsForType(PlayerUpgradeType.TANK);
        FuelTanksButton.onClick.AddListener(() => { 
            DisplayItemsForType(PlayerUpgradeType.TANK);
            ShopItemDescriptionView.DisplayInfo("Fuel Tanks", "Fuel tanks can increase your fuel capacity and allow you to stay underground for longer before having to refuel.");
        });
        DrillButton.onClick.AddListener(() => {
            DisplayItemsForType(PlayerUpgradeType.DRILL);
            ShopItemDescriptionView.DisplayInfo("Drills", "Drill upgrades allow you to dig faster. Gotta go fast!");
        });
    }

    private void DisplayItemsForType(PlayerUpgradeType type) {
        var upgradesOfType = items.Cast<PlayerUpgrade>().Where(u => u.Type == type).ToList();
        upgradesOfType.Sort();
        ShopItemListView.SetList(upgradesOfType, this.OnShopListItemSelected);
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
