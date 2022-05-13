using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonFacade : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button button;

    private UnityAction<object> callback;
    private object callbackData;

    void Start() {
        button.onClick.AddListener(this.OnClick);
    }

    public void SetListener(UnityAction<object> callback, object callbackData) {
        this.callback = callback;
        this.callbackData = callbackData;
    }

    public void SetLabel(string label) {
        text.text = label;
    }

    private void OnClick() {
        if(callback != null)
            callback.Invoke(callbackData);
    }
}
