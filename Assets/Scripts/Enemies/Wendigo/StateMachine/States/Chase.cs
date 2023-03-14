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

        public Chase(Wendigo wendigo)
        {
            data = wendigo.Data;
            rotationController = wendigo.GetController<WendigoRotationController>();
            movementController = wendigo.GetController<WendigoMovementController>();
            rangeCombatManager = wendigo.GetController<RangeCombatManager>();
        }

        public void Tick()
        {
            movementController.MoveForward();

            if (ShouldRotate)
                rotationController.RotateToTarget(data.Target.position);

            rangeCombatManager.PrepareToAttack();
        }

        private bool ShouldRotate => data.Movement.Velocity.magnitude > 1f;

        public void OnEnter()
        {
            LogEnter();
        }

        public void OnExit()
        {
            data.Movement.Velocity = Vector3.zero;
            LogExit();
        }
    }
}
