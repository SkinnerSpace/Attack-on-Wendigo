using UnityEngine;

public class FirebreathAttack : LoggableState, IState
{
    private WendigoData data;
    private WendigoMovementController movementController;
    private Firebreath firebreath;
    private FunctionTimer timer;

    public FirebreathAttack(Wendigo wendigo)
    {
        data = wendigo.Data;
        timer = wendigo.Timer;
        firebreath = wendigo.Firebreath;
        movementController = wendigo.GetController<WendigoMovementController>();
    }

    public void Tick()
    {
        movementController.Stop();
        firebreath.UpdateFire();
    }

    public void OnEnter()
    {
        LogEnter();
        firebreath.Fire();

        data.IsReadyToBreathFire = false;
        data.FirebreathAbilityIsCharged = false;
        timer.Set("FirebreathIsOver", 5f, () => data.FirebreathIsOver = true);
    }

    public void OnExit()
    {
        firebreath.Stop();

        LogExit();
        data.FirebreathIsOver = false;
        timer.Set("FirebreathIsRestored", 5f, () => data.FirebreathAbilityIsCharged = true);
    }
}