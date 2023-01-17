using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Arrival : IState
{
    private const float ARRIVAL_TIME = 3.2f;

    private FunctionTimer timer;
    private Wendigo wendigo;

    public Arrival(FunctionTimer timer, Wendigo wendigo)
    {
        this.timer = timer;
        this.wendigo = wendigo;
    }

    public void Tick() { }

    public void OnEnter()
    {
        timer.Set("Arrived", ARRIVAL_TIME, SetTarget);
    }

    public void OnExit() { }

    private void SetTarget()
    {
        wendigo.SetTarget(PlayerCharacter.Instance.transform);
    }
}


