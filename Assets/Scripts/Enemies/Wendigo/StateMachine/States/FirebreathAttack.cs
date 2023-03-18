namespace WendigoCharacter
{
    public class FirebreathAttack : LoggableState, IState
    {
        private FirebreathAbilityData data;
        private WendigoMovementController movementController;
        private WendigoAnimationController animationPlayer;
        private Firebreath firebreath;
        private FunctionTimer timer;

        public FirebreathAttack(Wendigo wendigo)
        {
            data = wendigo.Data.Firebreath;
            timer = wendigo.Timer;
            firebreath = wendigo.Firebreath;
            movementController = wendigo.GetController<WendigoMovementController>();
            animationPlayer = wendigo.GetController<WendigoAnimationController>();
        }

        public void Tick()
        {
            movementController.Stop();
            firebreath.UpdateFire();
        }

        public void OnEnter()
        {
            LogEnter();

            data.IsReadyToUse = false;
            data.IsCharged = false;
            animationPlayer.PlayFirebreathAnimation();
        }

        public void OnExit()
        {
            LogExit();
            data.IsOver = false;

            float time = Rand.Range(data.MinTime, data.MaxTime);
            timer.Set("FirebreathIsRestored", time, () => data.IsCharged = true);
        }
    }
}