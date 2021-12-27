using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Text Dialogue")]
[System.Serializable]
public class TextDialogue : Dialogue
{
    [SerializeField]
    private string Message;
    public override string GetMessage()
    {
        return Message;
    }
}
