using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Idle : LoggableState, IState
{
    private WendigoData data;

    private WendigoMovementController movementController;
    private WendigoRotationController rotationController;
    private WendigoRangeCombatManager rangeCombatManager;

    public Idle(Wendigo wendigo)
    {
        data = wendigo.Data;

        movementController = wendigo.GetController<WendigoMovementController>();
        rotationController = wendigo.GetController<WendigoRotationController>();
        rangeCombatManager = wendigo.GetController<WendigoRangeCombatManager>();
    }

    public void Tick()
    {
        movementController.Stop();
        rangeCombatManager.CheckReadinessToShoot();
    }

    public void OnEnter()
    {
        LogEnter();
    }

    public void OnExit()
    {
        LogExit();
    }
}
