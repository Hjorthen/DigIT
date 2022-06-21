using UnityEngine;
using UnityEngine.UI;

public class TransactionEntryView : MonoBehaviour
{
    [SerializeField]
    private float lifetime;
    [SerializeField]
    private float fadetime;
    private float maxFadetime;
    private Text text;

    void Start() 
    {
        maxFadetime = fadetime;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime; 
        if(lifetime <= 0) {
            fadetime -= Time.deltaTime;
            float fadeRatio = fadetime / maxFadetime;
            if(fadeRatio <= 0.1) {
                GameObject.Destroy(this.gameObject);
            }
            ApplyFadeEffect(fadeRatio);
        }
    }

    void ApplyFadeEffect(float fadeRatio) {
            Color color = text.color;
            color.a = fadeRatio;
            text.color = color;
    }
}
