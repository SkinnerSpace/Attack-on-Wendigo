using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoData : MonoBehaviour
    {
        public HealthData Health;
        public MovementData Movement;
        public FirebreathAbilityData Firebreath;
        public FireballAbilityData Fireball;
        public HeadData Head;
        public Transform Target;

        public TransformData Transform { get; private set; }

        private void Awake()
        {
            Transform = new TransformData(transform);
        }

        public bool IsActive { get; set; }
        public bool IsArrived { get; set; }
    }
}
