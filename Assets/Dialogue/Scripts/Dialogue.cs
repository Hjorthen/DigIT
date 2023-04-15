using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Dialogue : ScriptableObject
{
    public abstract IEnumerator<DialogueEntry> StartDialogue();
}
