using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableInteractionHandler : MonoBehaviour
{
    [SerializeField]
    private InteractionIndicatorView interactionIndicator;
    private InteractionEntity availableInteraction;

    public void SetAvailableInteraction(InteractionEntity entity) {
        availableInteraction = entity;
        if (availableInteraction != null) {
            interactionIndicator.Type = entity.Type;
        }
        else 
            interactionIndicator.Type = null;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            availableInteraction.Active = false;
        } else if(Input.GetKeyUp(KeyCode.E)) {
            availableInteraction.Active = true;
        }
    }
}
