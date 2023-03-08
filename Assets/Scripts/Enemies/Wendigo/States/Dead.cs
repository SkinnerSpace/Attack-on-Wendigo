using UnityEngine;

namespace WendigoCharacter
{
    public class Dead : LoggableState, IState
    {
        private WendigoData data;
        private CharacterController controller;

        public Dead(Wendigo wendigo)
        {
            data = wendigo.Data;
            controller = wendigo.Controller;
        }

        public void Tick() { }
        public void OnEnter()
        {
            data.Movement.Velocity = Vector3.zero;
            controller.enabled = false;
            LogEnter();
        }

        public void OnExit()
        {
            LogExit();
        }
    }
}
