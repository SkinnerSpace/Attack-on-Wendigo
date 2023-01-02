using System.Collections.Generic;
using UnityEngine;
using System;

public class FunctionTimer : MonoBehaviour
{
    private Dictionary<string, FunctionTimerInstance> activeTimers = new Dictionary<string, FunctionTimerInstance>();
    private Action onUpdate;

    private void Update()
    {
        onUpdate?.Invoke();
    }

    public void Set(string timerName, float time, Action action)
    {
        Set(timerName, time, new Clock(), action);
    }

    public void Set(string timerName, float time, IClock clock, Action action)
    {
        Stop(timerName);

        FunctionTimerInstance functionTimer = new FunctionTimerInstance(timerName, time, clock, action, this);
        onUpdate += functionTimer.Update;

        activeTimers.Add(timerName, functionTimer);
    }

    public void Stop(string timerName)
    {
        if (TimerExist(timerName))
            activeTimers[timerName].Stop();
    }

    public void RemoveTimer(string timerName)
    {
        if (TimerExist(timerName))
        {
            onUpdate -= activeTimers[timerName].Update;
            activeTimers.Remove(timerName);
        }
    }

    public bool TimerExist(string timerName)
    {
        return activeTimers.ContainsKey(timerName);
    }
}
