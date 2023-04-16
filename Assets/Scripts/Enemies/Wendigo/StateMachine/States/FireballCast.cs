
namespace WendigoCharacter
{
    public class FireballCast : LoggableState, IState
    {
        private WendigoData generalData;
        private FireballAbilityData data;
        private FunctionTimer timer;
        private WendigoMovementController movementController;
        private WendigoAnimationController animationPlayer;
        private WendigoSFXPlayer sFXPlayer;

        public FireballCast(Wendigo wendigo)
        {
            generalData = wendigo.Data;
            data = wendigo.Data.Fireball;
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

            generalData.IsRested = false;
            data.IsReadyToUse = false;
            data.IsCharged = false;
            animationPlayer.PlayCastAnimation();
            sFXPlayer.PlayShortRoarSFX();
        }

        public void OnExit()
        {
            LogExit();
            data.IsOver = false;

            float time = Rand.Range(data.MinTime, data.MaxTime);
            timer.Set("Recharge", time, () => data.IsCharged = true);
        }
    }
}
