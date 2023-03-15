public class ShakeTimer
{
    private float waitTime;
    private float currentTime;

    public ShakeTimer(float waitTime)
    {
        this.waitTime = waitTime;
        currentTime = 0f;
    }

    public void SetWaitTime(float waitTime) => this.waitTime = waitTime;

    public void CountDown() => currentTime += GlobalTime.DeltaTime;

    public float GetCompleteness()
    {
        float completeness = currentTime / waitTime;

        if (currentTime >= waitTime){
            currentTime = waitTime;
            completeness = 1f;
        }

        return completeness;
    }
}
