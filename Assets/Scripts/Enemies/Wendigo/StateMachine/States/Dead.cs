using UnityEngine;

namespace WendigoCharacter
{
    public class Dead : LoggableState, IState
    {
        private WendigoMover mover;

        public Dead(Wendigo wendigo)
        {
            mover = wendigo.Mover;
        }

        public void Tick() { }
        public void OnEnter()
        {
            mover.SwitchOff();
            LogEnter();
        }

        public void OnExit(){
            LogExit();
        }
    }
}
