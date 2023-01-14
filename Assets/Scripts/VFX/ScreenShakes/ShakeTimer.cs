using UnityEngine;

public class ShakeTimer
{
    public float TimeScaled => timeScaled;

    private float maxTime;
    private float time;
    private float timeScaled;

    public ShakeTimer(float maxTime)
    {
        this.maxTime = maxTime;

        time = 0f;
        timeScaled = 0f;
    }

    public bool TimeOut() => (time >= maxTime);

    public void CountDown()
    {
        time += Time.deltaTime;
        timeScaled = time / maxTime;

        if (time >= maxTime)
        {
            time = maxTime;
            timeScaled = 1f;
        }
    }
}
