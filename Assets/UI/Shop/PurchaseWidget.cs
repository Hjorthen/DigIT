using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseWidget : MonoBehaviour
{
    [SerializeField]
    private Button PurchaseButton;

    public delegate void PurchaseItemCallback(PlayerUpgrade item);
    private PurchaseItemCallback callback;
    private PlayerUpgrade item;

    void Start() {
        PurchaseButton.onClick.AddListener(this.OnButtonClicked);
    }

    public void SetItem(PlayerUpgrade item, PurchaseItemCallback callback) {
       this.item = item;
       this.callback = callback; 
    }

    private void OnButtonClicked() {
        if(callback != null) {
            callback.Invoke(item);
        }
    }
}
