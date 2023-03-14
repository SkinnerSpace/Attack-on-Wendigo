using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WendigoCharacter
{
    public class Idle : LoggableState, IState
    {
        private WendigoMovementController movementController;
        private RangeCombatManager rangeCombatManager;

        public Idle(Wendigo wendigo)
        {
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
            LogEnter();
        }

        public void OnExit()
        {
            LogExit();
        }
    }
}
