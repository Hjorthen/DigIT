using UnityEngine;

[CreateAssetMenu(menuName ="Text Dialogue")]
[System.Serializable]
public class TextDialogue : Dialogue
{
    [SerializeField, Multiline]
    private string Message;
    public override string GetMessage()
    {
        return Message;
    }
}
