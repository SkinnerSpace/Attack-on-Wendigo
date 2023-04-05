using System;
using UnityEngine;

public class DelayedAction
{
    private readonly Action execute;
    private readonly string name;
    private Action<string> onRemove; 

    public float time { get; private set; }
    
    public bool Active => active;
    private bool active;

    public DelayedAction(string name, float time, Action execute, Action<string> onRemove)
    {
        this.name = name;
        this.time = time;
        this.execute = execute;
        this.onRemove = onRemove;

        active = true;
    }

    public void Update(float deltaTime)
    {
        if (active)
        {
            time -= deltaTime;
            TimeOut();
        }
    }

    private void TimeOut()
    {
        if (time <= 0f)
        {
            Stop();
            execute();
        }
    }

    public float GetTimeLeft() => time;

    public void Stop()
    {
        active = false;
        onRemove(name);
    }
}
