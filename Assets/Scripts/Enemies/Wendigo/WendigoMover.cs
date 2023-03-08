using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoMover : MonoBehaviour
    {
        private Wendigo wendigo;
        private WendigoData data;
        private CharacterController controller;
        private IChronos chronos;

        public void Initialize(Wendigo wendigo)
        {
            controller = wendigo.Controller;
            chronos = wendigo.Chronos;
            data = wendigo.Data;
        }

        private void Update()
        {
            if (data.Health.IsAlive)
                controller.Move(data.Movement.Velocity * chronos.DeltaTime);
        }
    }
}
