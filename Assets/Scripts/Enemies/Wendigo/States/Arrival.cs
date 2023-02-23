using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Arrival : IState
{
    private const float ARRIVAL_TIME = 3.2f;
    private Wendigo wendigo;

    public Arrival(Wendigo wendigo) => this.wendigo = wendigo;

    public void Tick() { }

    public void OnEnter() => wendigo.Timer.Set("Arrival", ARRIVAL_TIME, OnArrived);

    public void OnExit() { }

    public void OnArrived() => wendigo.Data.IsArrived = true;
}


