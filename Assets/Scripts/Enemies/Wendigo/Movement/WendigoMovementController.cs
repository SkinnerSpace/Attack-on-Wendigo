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
            float percent = data.Movement.Acceleration * data.Movement.MULTIPLIER * chronos.DeltaTime;
            data.Movement.CurrentSpeed = Mathf.Lerp(data.Movement.CurrentSpeed, data.Movement.WalkSpeed, percent);
            data.Movement.Velocity = data.transform.forward * data.Movement.CurrentSpeed;
        }

        private void Decelerate()
        {
            float percent = data.Movement.Deceleration * data.Movement.MULTIPLIER * chronos.DeltaTime;
            data.Movement.Velocity = Vector3.Lerp(data.Movement.Velocity, Vector3.zero, percent);
        }

        private void NotifyOnVelocityUpdate()
        {
            float maxSpeed = data.Movement.DefaultWalkSpeed;
            data.Movement.DeltaWalkSpeed = data.Movement.Velocity.magnitude / maxSpeed;

            float clampedDeltaSpeed = Mathf.Clamp(data.Movement.DeltaWalkSpeed, 1f, 3f);

            onVelocityUpdate?.Invoke(clampedDeltaSpeed);
        }

        public void UpdateSpeed(float transition){
            UpdateWalkSpeed(transition);
            UpdateRotationSpeed(transition);
        }

        private void UpdateWalkSpeed(float transition)
        {
            float defaultWalkSpeed = data.Movement.DefaultWalkSpeed * data.Movement.MULTIPLIER;
            float injuredWalkSpeed = data.Movement.InjuredWalkSpeed * data.Movement.MULTIPLIER;

            data.Movement.WalkSpeed = Mathf.Lerp(defaultWalkSpeed, injuredWalkSpeed, transition);
        }

        private void UpdateRotationSpeed(float transition)
        {
            float defaultRotationSpeed = data.Movement.DefaultRotationSpeed * data.Movement.MULTIPLIER;
            float injuredRotationSpeed = data.Movement.InjuredRotationSpeed * data.Movement.MULTIPLIER;

            data.Movement.RotationSpeed = Mathf.Lerp(defaultRotationSpeed, injuredRotationSpeed, transition);
        }
    }
}
