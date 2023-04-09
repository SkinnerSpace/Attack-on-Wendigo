using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class FireballAbilityData : IRebootable
    {
        [Header("Distance")]
        public float MinDistance;
        public float MaxDistance;
        public float AngleOfView;

        [Header("Recharge")]
        public float MinTime;
        public float MaxTime;

        public bool OnTarget { get; set; }
        public bool IsExist { get; set; } = true;
        public bool IsReadyToUse { get; set; }
        public bool IsCharged { get; set; } = true;
        public bool IsOver { get; set; }

        public void Save()
        {
            
        }

        public void Reboot()
        {
            OnTarget = false;
            IsExist = true;
            IsReadyToUse = false;
            IsCharged = true;
            IsOver = false;
        }

    }
}

