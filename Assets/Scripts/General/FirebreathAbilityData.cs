using System;

namespace WendigoCharacter
{
    [Serializable]
    public class FirebreathAbilityData
    {
        public float MinDistance;
        public float MaxDistance;
        public float AngleOfView;

        public bool OnTarget { get; set; }
        public bool IsReadyToUse { get; set; }
        public bool IsCharged { get; set; } = true;
        public bool IsOver { get; set; }
    }
}

