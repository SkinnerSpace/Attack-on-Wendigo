using UnityEngine;

public class ShakeTimer
{
    public float TimeScaled => currentTimeScaled;

    private float waitTime;
    private float currentTime;
    private float currentTimeScaled;

    public ShakeTimer(float waitTime)
    {
        this.waitTime = waitTime;

        currentTime = 0f;
        currentTimeScaled = 0f;
    }

    public void SetWaitTime(float waitTime) => this.waitTime = waitTime;
    public bool TimeOut() => (currentTimeScaled >= 1f);

    public void CountDown()
    {
        currentTime += Time.deltaTime;
        CalculateScaledTime();
    }

    public void SetCompleteness(float progress) => currentTimeScaled = progress;

    private void CalculateScaledTime()
    {
        currentTimeScaled = currentTime / waitTime;

        if (currentTime >= waitTime)
        {
            currentTime = waitTime;
            currentTimeScaled = 1f;
        }
    }
}
