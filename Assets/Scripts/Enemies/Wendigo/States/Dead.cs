using UnityEngine;

public class Dead : LoggableState, IState
{
    private WendigoData data;

    public Dead(Wendigo wendigo)
    {
        data = wendigo.Data;
    }

    public void Tick() { }
    public void OnEnter()
    {
        data.Velocity = Vector3.zero;
        LogEnter();
    }

    public void OnExit()
    {
        LogExit();
    }
}
