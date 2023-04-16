using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WendigoCharacter
{
    public class Chase : LoggableState, IState
    {
        public const float ROTATION_THRESHOLD = 1f;

        private WendigoData data;
        private WendigoRotationController rotationController;
        private WendigoMovementController movementController;
        private RangeCombatManager rangeCombatManager;
        private WendigoAnimationController animationController;

        public Chase(Wendigo wendigo)
        {
            data = wendigo.Data;
            rotationController = wendigo.GetController<WendigoRotationController>();
            movementController = wendigo.GetController<WendigoMovementController>();
            rangeCombatManager = wendigo.GetController<RangeCombatManager>();
            animationController = wendigo.GetController<WendigoAnimationController>();
        }

        public void Tick()
        {
            movementController.MoveForward();
            rotationController.RotateToTarget(data.Target.Position);

            rangeCombatManager.PrepareToAttack();
        }

        public void OnEnter()
        {
            animationController.SetIsWalking(true);
            LogEnter();
        }

        public void OnExit()
        {
            animationController.SetIsWalking(false);
            data.Movement.Velocity = Vector3.zero;
            data.Movement.CurrentSpeed = 0f;
            LogExit();
        }
    }
}
