using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoData : MonoBehaviour
    {
        [SerializeField] private WendigoSerializableData serializable;
        public HealthData Health;
        public MovementData Movement;
        public FirebreathAbilityData Firebreath;
        public FireballAbilityData Fireball;

        public TransformData Transform { get; private set; }

        private void Awake()
        {
            Transform = new TransformData(transform);
        }

        public bool IsActive { get; set; }
        public bool IsArrived { get; set; }


        public Transform Target { get { return serializable.Target; } set { serializable.Target = value; } }
        public bool TargetFitsLookAngle { get; set; }
        public float LookAngleOfView => serializable.LookAngleOfView;

        public float FireRange => serializable.FireRange;
        public float FireScatter => serializable.FireScatter;
        public int FirePrecision => serializable.FirePrecision;
    }
}
