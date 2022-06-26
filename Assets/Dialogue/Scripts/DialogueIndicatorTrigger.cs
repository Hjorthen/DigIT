using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueIndicatorTrigger : MonoBehaviour
{
    [SerializeField]
    private Collider2D DialogueCollider;
    [SerializeField]
    private GameObject indicator;
    void OnTriggerEnter2D(Collider2D collider) {
        RefreshIndicator();
    }

    void Start() {
        RefreshIndicator();
    }

    void OnTriggerExit2D(Collider2D collider) {
        RefreshIndicator();
    }

    private void RefreshIndicator() {
        bool indicatorVisible = DialogueCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        indicator.SetActive(indicatorVisible);
    }
}
