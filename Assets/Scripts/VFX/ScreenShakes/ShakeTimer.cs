using UnityEngine;

public class ShakeTimer : MonoBehaviour
{
    public float TimeScaled => timeScaled;

    private float maxTime;
    private float time;
    private float timeScaled;

    [SerializeField] private ScreenShake observer;

    public void Set(float maxTime)
    {
        this.maxTime = maxTime;

        time = 0f;
        timeScaled = 0f;
    }

    public void CountDown(float timePassed)
    {
        time += timePassed;
        timeScaled = time / maxTime;

        if (time >= maxTime)
        {
            time = maxTime;
            timeScaled = 1f;
            observer.Stop();
        }
    }
}
