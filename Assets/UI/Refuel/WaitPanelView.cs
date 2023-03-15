using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitPanelView : MonoBehaviour
{
    [SerializeField]
    private Text label;
    public bool Visible {
        get => gameObject.activeInHierarchy;
        set {
            gameObject.SetActive(value);
        }
    }

    void Start() {
        DisplaySecondsRemaining = float.NaN;
    }

    public float DisplaySecondsRemaining {
        set {
            string secondsFormatted = string.Format("Waiting .. {0, 4:F0}s", value);
            label.text = secondsFormatted;
        }
    }
}
