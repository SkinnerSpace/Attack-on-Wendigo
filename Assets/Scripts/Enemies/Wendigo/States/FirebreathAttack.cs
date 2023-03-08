using UnityEngine;

namespace WendigoCharacter
{
    public class FirebreathAttack : LoggableState, IState
    {
        private WendigoData data;
        private WendigoMovementController movementController;
        private WendigoAnimationController animationPlayer;
        private Firebreath firebreath;
        private FunctionTimer timer;

        private float restoreTime = 5f;

        public FirebreathAttack(Wendigo wendigo)
        {
            data = wendigo.Data;
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

            data.Firebreath.IsReadyToUse = false;
            data.Firebreath.IsCharged = false;
            animationPlayer.PlayFirebreathAnimation();
        }

        public void OnExit()
        {
            LogExit();
            data.Firebreath.IsOver = false;
            timer.Set("FirebreathIsRestored", restoreTime, () => data.Firebreath.IsCharged = true);
        }
    }
}