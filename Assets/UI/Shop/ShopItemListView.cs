using System.Collections.Generic;
using UnityEngine;

public class ShopItemListView : MonoBehaviour
{
    [SerializeField]
    private RectTransform Root;
    [SerializeField]
    private ButtonFacade Prefab;
    public delegate void ItemClickedCallback(PlayerUpgrade item);
    private ItemClickedCallback shopCallback;

    public void SetList(ICollection<PlayerUpgrade> items, ItemClickedCallback callback) {
        for (int i = 0; i < Root.childCount; i++) {
            GameObject.Destroy(Root.GetChild(i).gameObject);
        }
        foreach(PlayerUpgrade item in items) {
            AddItem(item, callback);
        }
    }

    private void AddItem(PlayerUpgrade item, ItemClickedCallback callback) {
        GameObject itemObject = GameObject.Instantiate(Prefab.gameObject, Root);
        ButtonFacade label = itemObject.GetComponent<ButtonFacade>();

        label.SetLabel(item.Name);
        shopCallback = callback;
        label.SetListener(this.ButtonCallback, (object)item);
    }

    private void ButtonCallback(object metadata) {
        shopCallback.Invoke((PlayerUpgrade)metadata);
    }
}
