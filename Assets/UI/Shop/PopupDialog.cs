using UnityEngine;
using UnityEngine.UI;

public class PopupDialog : MonoBehaviour
{
    [SerializeField]
    private Text text;
    public void Show(string message) {
        text.text = message;
        gameObject.SetActive(true);
    }
}
