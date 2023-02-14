using System.Collections.Generic;
using UnityEngine;
using System;

public class FunctionTimer : MonoBehaviour, IFunctionTimer
{
    [SerializeField] private Chronos chronos;
    private Dictionary<string, DelayedAction> delayedActions = new Dictionary<string, DelayedAction>();

    private event Action<float> onTick;

    private void Update() => onTick?.Invoke(chronos.DeltaTime);

    public void Set(string timerName, float time, Action action)
    {
        Stop(timerName);

        DelayedAction delayedAction = new DelayedAction(timerName, time, action, RemoveTimer);
        onTick += delayedAction.Update;

        delayedActions.Add(timerName, delayedAction);
    }

    public void Stop(string timerName)
    {
        if (TimerExist(timerName))
            delayedActions[timerName].Stop();
    }

    public void RemoveTimer(string timerName)
    {
        if (TimerExist(timerName))
        {
            onTick -= delayedActions[timerName].Update;
            delayedActions.Remove(timerName);
        }
    }

    public bool TimerExist(string timerName) => delayedActions.ContainsKey(timerName);
    public float GetTimeLeft(string timeName) => delayedActions[timeName].GetTimeLeft();
}
