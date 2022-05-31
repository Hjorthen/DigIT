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
        string header = $"{item.Name} - {item.BasePrice}$";
        DisplayInfo(header, "");
    }

    public void DisplayInfo(string header, string body) {
        TextLabel.text = $"<b>{header}</b>\n\n{body}";
    }
}
