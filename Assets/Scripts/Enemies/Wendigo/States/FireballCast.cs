public class FireballCast : LoggableState, IState
{
    private WendigoData data;
    private FunctionTimer timer;
    private WendigoMovementController movementController;
    private WendigoAnimationPlayer animationPlayer;
    private WendigoSFXPlayer sFXPlayer;

    public FireballCast(Wendigo wendigo)
    {
        data = wendigo.Data;
        timer = wendigo.Timer;
        movementController = wendigo.GetController<WendigoMovementController>();
        animationPlayer = wendigo.GetController<WendigoAnimationPlayer>();
        sFXPlayer = wendigo.SFXPlayer;
    }

    public void Tick()
    {
        movementController.Stop();
    }

    public void OnEnter()
    {
        LogEnter();

        data.IsReadyToCast = false;
        data.FireballAbilityIsCharged = false;
        animationPlayer.PlayCastAnimation();
        sFXPlayer.PlayShortRoarSFX();
    }

    public void OnExit()
    {
        LogExit();
        data.FireballCastIsOver = false;
        timer.Set("Recharge", data.FireballChargeTime, () => data.FireballAbilityIsCharged = true);
    }
}
