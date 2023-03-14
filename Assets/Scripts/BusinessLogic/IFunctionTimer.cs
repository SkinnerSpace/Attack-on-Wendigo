using System;

public interface IFunctionTimer
{
    void Set(string timerName, float time, Action action);
    float GetTimeLeft(string timerName);
}
