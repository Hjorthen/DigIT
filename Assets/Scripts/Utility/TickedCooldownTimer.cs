using UnityEngine;

public class TickedCooldownTimer : ICooldownTimer
{
    public float RemainingCooldown {
        private set;
        get;
    }
    public bool Expired => RemainingCooldown <= 0;

    public void WaitFor(float seconds)
    {
        RemainingCooldown = seconds;
    }

    public void AdvanceBy(float delta) {
        RemainingCooldown = Mathf.Max(0.0f, RemainingCooldown - delta);
    }
}
