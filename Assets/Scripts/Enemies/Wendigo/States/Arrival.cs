using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Arrival : LoggableState, IState
{
    private const float ARRIVAL_TIME = 3.2f;
    private Wendigo wendigo;

    public Arrival(Wendigo wendigo) => this.wendigo = wendigo;

    public void Tick() { }

    public void OnEnter()
    {
        LogEnter();
        wendigo.Timer.Set("Arrival", ARRIVAL_TIME, OnArrived);
    }

    public void OnExit()
    {
        LogExit();
    }

    public void OnArrived() => wendigo.Data.IsArrived = true;
}
