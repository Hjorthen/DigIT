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
    private GameObject WaitPanel;
    [SerializeField]
    private TickedCooldownTimer timer = new TickedCooldownTimer();

    public void OnWaitButtonClicked() {
        WaitPanel.SetActive(true);
        timer.WaitFor(5);
    }

    public void OnSelfDestructClicked() {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerStats = player.GetComponent<PlayerStats>();
        playerStats.Hull.Currentvalue = 0;
    }

    public void OnWaitCancelled() {
        WaitPanel.SetActive(false);
    }

    public void DisplayRefuelPanel() {
        gameObject.SetActive(true);
        RefuelPanel.SetActive(true);
        WaitPanel.SetActive(false);
    }

    public void HideRefuelPanel() {
        gameObject.SetActive(false);
        RefuelPanel.SetActive(false);
        WaitPanel.SetActive(false);
    }

    void Update()
    {
        timer.AdvanceBy(Time.deltaTime);
        Debug.Log(WaitPanel.activeInHierarchy + " " + timer.Expired);
        if(WaitPanel.activeInHierarchy && timer.Expired) {
            int roll = Random.Range(0, 3);
            Debug.Log(roll);
            if(roll == 0) {
                DialogueController.Play(CreateRefuelDialogue());
                HideRefuelPanel();
            } else {
                timer.WaitFor(5);
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
