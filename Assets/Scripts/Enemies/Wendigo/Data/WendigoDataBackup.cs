using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoDataBackup
    {
        public HealthData Health { get; private set; }
        public MovementData Movement { get; private set; }
        public FirebreathAbilityData Firebreath { get; private set; }
        public FireballSpawnerData FireballSpawner { get; private set; }
        public FireballAbilityData Fireball { get; private set; }
        public HeadData Head { get; private set; }
        public WendigoTarget Target { get; private set; }
        public TransformData Transform { get; private set; }
    }
}
