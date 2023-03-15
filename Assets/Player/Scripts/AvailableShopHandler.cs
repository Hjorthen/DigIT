using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableShopHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject shopAvailableIndicator;
    private GameObject currentAvailableShop;

    public void SetAvailableShop(GameObject root) {
        currentAvailableShop = root;
        if (root != null)
            shopAvailableIndicator.SetActive(true);
        else 
            shopAvailableIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && currentAvailableShop.activeInHierarchy) {
            currentAvailableShop.SetActive(false);
            Time.timeScale = 1;
        } else if(Input.GetKeyUp(KeyCode.E) && !currentAvailableShop.activeInHierarchy) {
            currentAvailableShop.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
