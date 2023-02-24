using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Idle : LoggableState, IState
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
