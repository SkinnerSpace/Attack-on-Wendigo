using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class FirebreathAbilityData : IRebootable
    {
        [Header("Distance")]
        public float MinDistance;
        public float MaxDistance;
        public float AngleOfView;

        [Header("Recharge")]
        public float MinTime;
        public float MaxTime;

        private float init_minDistance;
        private float init_maxDistance;
        private float init_angleOfView;

        private float init_minTime;
        private float init_maxTime;

        public FirebreathColliderData Collider;
        public FirebreathColliderRendererData ColliderRenderer;

        public bool OnTarget { get; set; }
        public bool IsReadyToUse { get; set; }
        public bool IsCharged { get; set; } = true;
        public bool IsOver { get; set; }

        public void Save()
        {
            init_minDistance = MinDistance;
            init_maxDistance = MaxDistance;
            init_angleOfView = AngleOfView;

            init_minTime = MinTime;
            init_maxTime = MaxTime;
        }

        public void Reboot()
        {
            MinDistance = init_minDistance;
            MaxDistance = init_maxDistance;
            AngleOfView = init_angleOfView;

            MinTime = init_minTime;
            MaxTime = init_maxTime;

            OnTarget = false;
            IsReadyToUse = false;
            IsCharged = true;
            IsOver = false;
        }
    }
}

