using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            GameConsole.WriteLine(dialogue.GetMessage());
        }
    }
}
