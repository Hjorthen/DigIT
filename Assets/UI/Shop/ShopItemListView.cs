using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemListView : MonoBehaviour
{
    [SerializeField]
    private RectTransform Root;
    [SerializeField]
    private ButtonFacade Prefab;
    public delegate void ItemClickedCallback(PlayerUpgrade item);
    private ItemClickedCallback shopCallback;

    public void AddItem(PlayerUpgrade item, ItemClickedCallback callback) {
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
