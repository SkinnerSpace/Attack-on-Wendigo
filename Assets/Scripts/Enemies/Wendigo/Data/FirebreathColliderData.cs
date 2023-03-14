using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class FirebreathColliderData
    {
        [Header("Collision")]
        [Range(0, 360)]
        public float FOVDeg = 45;
        public float RadiusOuter = 10f;
        public float RadiusInner = 1f;
        public float DistanceOffset;

        [Header("Observable Area")]
        public float ObservableRadius = 10f;
        public float ExpansionTime = 1f;
        public float CurrentExpansionTime = 0f;
        [Range(0f, 1f)]
        public float ObservableExpansion = 0f;
        public int CollidersLimit = 16;

        public float FOVRad { get; set; }
        public Vector3 ObservableCenter { get; set; }
        public Vector3 CollisionCenter { get; set; }
    }
}

