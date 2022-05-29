using UnityEngine;


[System.Serializable]
public class ShopEnteredHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject shopView;

    public void OnTriggerEnter2D(Collider2D collider) {
        var playerController = collider.GetComponent<PlayerMiningController>();
        if(playerController != null) {
            shopView.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.Escape) && shopView?.activeInHierarchy == true) {
            shopView.SetActive(false);
            Time.timeScale = 1;
        }
    }
}