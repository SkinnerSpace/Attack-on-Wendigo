
namespace WendigoCharacter
{
    public class FireballCast : LoggableState, IState
    {
        private WendigoData data;
        private FunctionTimer timer;
        private WendigoMovementController movementController;
        private WendigoAnimationController animationPlayer;
        private WendigoSFXPlayer sFXPlayer;

        public FireballCast(Wendigo wendigo)
        {
            data = wendigo.Data;
            timer = wendigo.Timer;
            movementController = wendigo.GetController<WendigoMovementController>();
            animationPlayer = wendigo.GetController<WendigoAnimationController>();
            sFXPlayer = wendigo.SFXPlayer;
        }

        public void Tick()
        {
            movementController.Stop();
        }

        public void OnEnter()
        {
            LogEnter();

            data.Fireball.IsReadyToUse = false;
            data.Fireball.IsCharged = false;
            animationPlayer.PlayCastAnimation();
            sFXPlayer.PlayShortRoarSFX();
        }

        public void OnExit()
        {
            LogExit();
            data.Fireball.IsOver = false;
            timer.Set("Recharge", data.Fireball.RechargeTime, () => data.Fireball.IsCharged = true);
        }
    }
}
