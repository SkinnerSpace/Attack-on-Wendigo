using UnityEngine;

namespace WendigoCharacter{
    public class Disabled : LoggableState, IState
    {
        public void Tick() { }

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