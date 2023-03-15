using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RefuelPresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject RefuelPanel;

    [SerializeField]
    private DialogueController DialogueController;

    [SerializeField]
    private WaitPanelView WaitPanel;
    [SerializeField]
    private TickedCooldownTimer timer = new TickedCooldownTimer();

    public void OnWaitButtonClicked() {
        WaitPanel.Visible = true;
        const float minWaitSeconds = 20;
        const float maxWaitSeconds = 90;
        timer.WaitFor(Random.Range(minWaitSeconds, maxWaitSeconds));
    }

    public void OnSelfDestructClicked() {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerStats = player.GetComponent<PlayerStats>();
        playerStats.Hull.Currentvalue = 0;
    }

    public void OnWaitCancelled() {
        WaitPanel.Visible = false;
    }

    public void DisplayRefuelPanel() {
        gameObject.SetActive(true);
        RefuelPanel.SetActive(true);
        WaitPanel.Visible = false;
    }

    public void HideRefuelPanel() {
        gameObject.SetActive(false);
        RefuelPanel.SetActive(false);
        WaitPanel.Visible = false;
    }

    void Update()
    {
        timer.AdvanceBy(Time.deltaTime);
        if(WaitPanel.Visible) {
            WaitPanel.DisplaySecondsRemaining = timer.RemainingCooldown;
            if(timer.Expired) {
                DialogueController.Play(CreateRefuelDialogue());
                HideRefuelPanel();
            }
        }
    }

    private IEnumerator<DialogueEntry> CreateRefuelDialogue() {
        string actorName = "Good Samaritan";
        yield return new DialogueEntry(actorName, "Howdy!\nI picked up on your emergency beacon. You seem to be out of fuel?");
        yield return new DialogueEntry(actorName, "Very well. I just so happen to have come straight from the surface. I don't mind sparing some for free. Just remember this day if you ever run into someone in your situation in the future!");
        var playerFuel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Fuel;
        playerFuel.Currentvalue = Mathf.Min(playerFuel.MaxValue * 0.2f, 50);
        yield return new DialogueEntry(actorName, "Good luck!");
    }
}
