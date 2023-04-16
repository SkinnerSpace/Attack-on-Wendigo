using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class MovementData : IRebootable
    {
        [Header("Walk")]
        public float DefaultWalkSpeed;
        public float InjuredWalkSpeed;
        public float WalkSpeed;
        public float DeltaWalkSpeed;

        public float CurrentSpeed;
        public float Acceleration;
        public float Deceleration;

        [Header("Rotation")]
        public float DefaultRotationSpeed;
        public float InjuredRotationSpeed;
        public float RotationSpeed;

        public Vector3 Velocity { get; set; }

        private float init_walkSpeed;
        private float init_rotationSpeed;
        private float init_decelration;

        public void Save()
        {
            init_walkSpeed = WalkSpeed;
            init_rotationSpeed = RotationSpeed;
            init_decelration = Deceleration;
        }

        public void Reboot()
        {
            WalkSpeed = init_walkSpeed;
            RotationSpeed = init_rotationSpeed;
            Deceleration = init_decelration;
        }
    }
}
