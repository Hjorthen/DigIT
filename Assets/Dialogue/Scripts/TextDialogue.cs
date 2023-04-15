using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Text Dialogue")]
[System.Serializable]
public class TextDialogue : Dialogue
{
    [SerializeField]
    private string _ActorName;
    [SerializeField, Multiline]
    private string Message;

    public override IEnumerator<DialogueEntry> StartDialogue()
    {
        yield return new DialogueEntry() { actorName = _ActorName, text = Message};
    }
}
