using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoMover : MonoBehaviour
    {
        [SerializeField] private WendigoData data;
        [SerializeField] private CharacterController controller;
        [SerializeField] private Chronos chronos;

        public void SwitchOn() => controller.enabled = true;

        public void SwitchOff(){
            controller.enabled = false;
            data.Movement.Velocity = Vector3.zero;
        }

        private void Update()
        {
            if (data.Health.IsAlive)
                controller.Move(data.Movement.Velocity * chronos.DeltaTime);
        }
    }
}
