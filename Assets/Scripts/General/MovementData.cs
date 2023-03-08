using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class MovementData
    {
        public float WalkSpeed;
        public float RotationSpeed;
        public float Deceleration;

        public Vector3 Velocity { get; set; }
    }
}
