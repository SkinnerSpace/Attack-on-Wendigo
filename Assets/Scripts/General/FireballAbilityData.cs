using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class FireballAbilityData
    {
        public float MinDistance;
        public float MaxDistance;
        public float AngleOfView;
        public float RechargeTime;

        public bool OnTarget { get; set; }
        public bool IsReadyToUse { get; set; }
        public bool IsCharged { get; set; } = true;
        public bool IsOver { get; set; }
    }
}

