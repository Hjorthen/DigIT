using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionIndicatorView : MonoBehaviour
{
    [SerializeField]
    private TextMesh interactionLabel;

    private bool Visible {
        set {
            gameObject.SetActive(value);
        }
    }

    public InteractionEntity.InteractionType? Type {
        set {
            if (value != null) {
                UpdateInteractionLabel((InteractionEntity.InteractionType)value);
                Visible = true;
            } else {
                Visible = false;
            }
        }
    }

    private void UpdateInteractionLabel(InteractionEntity.InteractionType type) {
        string typeLabel = LookupInteractionVerb(type);
        interactionLabel.text = string.Format("{0} (E)", typeLabel);
    }

    private string LookupInteractionVerb(InteractionEntity.InteractionType type) {
        switch (type)
        {
            case InteractionEntity.InteractionType.DIALOGUE:
                return "Talk";
            case InteractionEntity.InteractionType.SHOP:
                return "Shop";
        }
        throw new System.ArgumentException($"{type} is not a known interaction type.", nameof(type));
    }
}
