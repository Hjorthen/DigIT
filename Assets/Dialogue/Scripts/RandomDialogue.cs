using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Random Dialogue")]
[System.Serializable]
public class RandomDialogue : Dialogue
{
    [SerializeField]
    private List<Dialogue> Dialogues;
    
    public override string GetMessage()
    {
        int randomIndex = Random.Range(0, Dialogues.Count - 1);
        return Dialogues[randomIndex].GetMessage();
    }
}
