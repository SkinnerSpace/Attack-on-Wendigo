using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoMovementController : WendigoPlugableComponent
    {
        private WendigoData data;
        private IChronos chronos;

        public Action<float> onVelocityUpdate;

        public override void Initialize(Wendigo wendigo)
        {
            data = wendigo.Data;
            chronos = wendigo.Chronos;
        }

        public void MoveForward()
        {
            Accelerate();
            Decelerate();
            NotifyOnVelocityUpdate();
        }

        public void Stop()
        {
            Decelerate();
        }

        private void Accelerate()
        {
            data.Movement.CurrentSpeed = Mathf.Lerp(data.Movement.CurrentSpeed, data.Movement.WalkSpeed, data.Movement.Acceleration * chronos.DeltaTime);
            data.Movement.Velocity = data.transform.forward * data.Movement.CurrentSpeed;
        }

        private void Decelerate()
        {
            float percent = data.Movement.Deceleration * chronos.DeltaTime;
            data.Movement.Velocity = Vector3.Lerp(data.Movement.Velocity, Vector3.zero, percent);
        }

        private void NotifyOnVelocityUpdate()
        {
            data.Movement.DeltaWalkSpeed = data.Movement.Velocity.magnitude / data.Movement.DefaultWalkSpeed;
            float clampedDeltaSpeed = Mathf.Clamp(data.Movement.DeltaWalkSpeed, 1f, 2f);
            onVelocityUpdate?.Invoke(clampedDeltaSpeed);
        }

        public void UpdateWalkSpeed(float degree)
        {
            data.Movement.WalkSpeed = Mathf.Lerp(data.Movement.DefaultWalkSpeed, data.Movement.InjuredWalkSpeed, degree);
            data.Movement.RotationSpeed = Mathf.Lerp(data.Movement.DefaultRotationSpeed, data.Movement.InjuredRotationSpeed, degree);
        }
    }
}
