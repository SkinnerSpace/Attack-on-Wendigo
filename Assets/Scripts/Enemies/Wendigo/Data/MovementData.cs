using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class MovementData : IRebootable
    {
        public float WalkSpeed;
        public float RotationSpeed;
        public float Deceleration;

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
