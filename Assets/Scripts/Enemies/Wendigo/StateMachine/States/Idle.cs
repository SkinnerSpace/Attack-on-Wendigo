using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WendigoCharacter
{
    public class Idle : LoggableState, IState
    {
        private const float REST_TIME = 0.5f;

        private WendigoData data;
        private FunctionTimer timer;
        private WendigoMovementController movementController;
        private RangeCombatManager rangeCombatManager;

        public Idle(Wendigo wendigo)
        {
            data = wendigo.Data;
            timer = wendigo.Timer;
            movementController = wendigo.GetController<WendigoMovementController>();
            rangeCombatManager = wendigo.GetController<RangeCombatManager>();
        }

        public void Tick()
        {
            movementController.Stop();
            rangeCombatManager.PrepareToAttack();
        }

        public void OnEnter()
        {
            timer.Set("Rest", REST_TIME, OnRested);
            LogEnter();
        }

        private void OnRested(){
            data.IsRested = true;
        }

        public void OnExit()
        {
            LogExit();
        }
    }
}
