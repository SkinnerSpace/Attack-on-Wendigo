using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WendigoCharacter
{
    public class Arrival : LoggableState, IState
    {
        private const float ARRIVAL_TIME = 3.2f;
        private WendigoData data;
        private IFunctionTimer timer;

        private RangeCombatManager rangeCombatManager;

        public Arrival(Wendigo wendigo)
        {
            data = wendigo.Data;
            timer = wendigo.Timer;

            rangeCombatManager = wendigo.GetController<RangeCombatManager>();
        }

        public void Tick()
        {
            rangeCombatManager.PrepareToAttack();
        }

        public void OnEnter()
        {
            LogEnter();
            timer.Set("Arrival", ARRIVAL_TIME, OnArrived);
        }

        public void OnExit()
        {
            LogExit();
        }

        public void OnArrived() => data.IsArrived = true;
    }
}
