using UnityEngine;

public class SinCounter
{
    private const float MAX_SIN_TIME = Mathf.PI * 2f;

    public float time { get; private set; }

    public void CountTime(float frequency)
    {
        time += frequency * Time.deltaTime;

        if (time > MAX_SIN_TIME)
            time -= MAX_SIN_TIME;
    }
}
