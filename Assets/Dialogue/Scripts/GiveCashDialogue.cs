using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/GiveCashDialogue")]
public class GiveCashDialogue : Dialogue
{
    [SerializeField]
    private int _PayoutAmount;
    [SerializeField]
    private string _ActorName;
    [SerializeField]
    private string _PayoutMessage;
    [SerializeField, Tooltip("The message to be shown right after the money has been paid.")]
    private string _PayoutMessageTwo;
    [SerializeField]
    private string _PostPayoutMessage;
    private bool _Paid = false;
    public override IEnumerator<DialogueEntry> StartDialogue()
    {
        if(!_Paid) {
            yield return new DialogueEntry() { actorName = _ActorName, text = _PayoutMessage };
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerStats stats = player.GetComponent<PlayerStats>();
            stats.Currency.Withdraw(-_PayoutAmount);
            _Paid = true;
            yield return new DialogueEntry() { actorName = _ActorName, text = _PayoutMessageTwo };
        } else {
            yield return new DialogueEntry() { actorName = _ActorName, text = _PostPayoutMessage };
        }
    }
}
