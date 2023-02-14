using System;

public class MockFunctionTimer : IFunctionTimer
{
    private string timerName;
    private float time;
    private Action action;

    public float GetTimeLeft(string timerName) => time;
    public void Set(string timerName, float time, Action action)
    {
        this.timerName = timerName;
        this.time = time;
        this.action = action;
    }
    public void TimeOut() => action();
}
