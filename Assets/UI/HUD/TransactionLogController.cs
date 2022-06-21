using UnityEngine;
using UnityEngine.UI;

public class TransactionLogController : MonoBehaviour
{
    [SerializeField]
    private GameObject logEntryPrefab;
    [SerializeField]
    private Transform root;
    public void AddEntry(string text) {
        GameObject newObject = GameObject.Instantiate(logEntryPrefab, root);
        Text textComponent = newObject.GetComponent<Text>();
        textComponent.text = text;
    }
}
