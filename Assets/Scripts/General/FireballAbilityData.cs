using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class FireballAbilityData
    {
        [Header("Distance")]
        public float MinDistance;
        public float MaxDistance;
        public float AngleOfView;

        [Header("Recharge")]
        public float MinTime;
        public float MaxTime;

        public bool OnTarget { get; set; }
        public bool IsReadyToUse { get; set; }
        public bool IsCharged { get; set; } = true;
        public bool IsOver { get; set; }
    }
}

