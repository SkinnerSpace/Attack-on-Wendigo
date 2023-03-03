using UnityEngine;

public class FireballLaunch : LoggableState, IState
{
    private Wendigo wendigo;
    private WendigoData data;
    private FunctionTimer timer;
    private WendigoRangeCombatManager rangeCombatManager;
    private WendigoMovementController movementController;
    private WendigoAnimatorController animatorController;
    private FireballSpawner fireballSpawner;

    private float chargeTime = 1f;

    public FireballLaunch(Wendigo wendigo)
    {
        this.wendigo = wendigo;
        data = wendigo.Data;
        timer = wendigo.Timer;
        fireballSpawner = wendigo.FireballSpawner;
        rangeCombatManager = wendigo.GetController<WendigoRangeCombatManager>();
        movementController = wendigo.GetController<WendigoMovementController>();
        animatorController = wendigo.GetController<WendigoAnimatorController>();
    }

    public void Tick()
    {
        movementController.Stop();
        rangeCombatManager.CheckReadinessToShoot();
    }

    public void OnEnter()
    {
        animatorController.PlayCastAnimation();
        //timer.Set("Launch", data.FireballCastTime, Launch);
    }

    public void OnExit()
    {
        timer.Set("Recharge", data.FireballChargeTime, () => data.FireballIsReady = true);
    }

    private void Launch()
    {
        /*fireballSpawner.SpawnFireball();
        data.FireballIsReady = false;
        timer.Set("Recharge", data.FireballChargeTime, () => data.FireballIsReady = true);*/
    }
}
