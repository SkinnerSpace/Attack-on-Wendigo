using System.Collections;
using UnityEngine;
using System;

public class FunctionTimerInstance
{
    private readonly Action execute;
    private readonly string timerName;
    private readonly IClock clock;
    private readonly FunctionTimer host;

    private float time;
    
    public bool Active => active;
    private bool active;

    public FunctionTimerInstance(string timerName, float time, IClock clock, Action execute, FunctionTimer host)
    {
        this.timerName = timerName;
        this.time = time;
        this.clock = clock;
        this.execute = execute;
        this.host = host;

        active = true;
    }

    public void Update()
    {
        if (active)
        {
            time -= clock.DeltaTime;

            if (time < 0f)
            {
                execute();
                Stop();
            }
        }
    }

    public void Stop()
    {
        active = false;
        host.RemoveTimer(timerName);
    }
}
