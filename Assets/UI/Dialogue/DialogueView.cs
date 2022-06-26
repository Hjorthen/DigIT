using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    [SerializeField]
    private Text textField;
    [SerializeField]
    private Text actorNameField;
    [SerializeField]
    private GameObject dialogueArea;

    private UnityAction dialogueEndCallback;
    private IEnumerator<DialogueEntry> dialogueDriver;

    public void Display(string actorName, string text, UnityAction onDialogueEndCallback) {
        UpdateViewText(text);
        UpdateActorField(actorName);
        DisplayDialogueArea(true);
        dialogueEndCallback = onDialogueEndCallback;
        Time.timeScale = 0.0f;
    }
    private void Display(DialogueEntry dialogue) {
        Display(dialogue.actorName, dialogue.text);
    }

    public void Display(IEnumerator<DialogueEntry> enumerable) {
        dialogueDriver = enumerable;
        dialogueDriver.MoveNext();
        Display(dialogueDriver.Current);
    }

    public void Display(string actorName, string text) {
        Display(actorName, text, null);
    }

    void Update() {
        if(Input.GetButtonDown("Submit")) {
            if(dialogueDriver.MoveNext()) {
                Display(dialogueDriver.Current);
            }
            else {
                ClearDialogueArea();
                if(dialogueEndCallback != null)
                    dialogueEndCallback();
                dialogueEndCallback = null;
                Time.timeScale = 1.0f;
            }
        }
    }

    private void ClearDialogueArea() {
        DisplayDialogueArea(false);
        UpdateViewText("");
        UpdateActorField("");
    }

    private void UpdateViewText(string text) {
        textField.text = text;
    }
    private void UpdateActorField(string actorName) {
        actorNameField.text = actorName;
    }
    private void DisplayDialogueArea(bool enable) {
        dialogueArea.SetActive(enable);
    }
}
