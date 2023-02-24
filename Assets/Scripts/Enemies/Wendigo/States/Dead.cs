using UnityEngine;

public class Dead : LoggableState, IState
{
    public void Tick() { }
    public void OnEnter()
    {
        LogEnter();
    }

    public void OnExit()
    {
        LogExit();
    }
}
