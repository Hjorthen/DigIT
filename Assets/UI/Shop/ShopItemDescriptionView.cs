using UnityEngine;
using UnityEngine.UI;

public class ShopItem {
    public string Name;
    public string Description;
}

public class ShopItemDescriptionView : MonoBehaviour
{
    [SerializeField]
    private Text TextLabel;
    public void DisplayItem(PlayerUpgrade item) {
        TextLabel.text = $"<b>{item.Name}</b>";
    }
}
