using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoMovementController : WendigoPlugableComponent
    {
        private WendigoData data;
        private IChronos chronos;

        public Action<float> onVelocityUpdate;

        public override void Initialize(IWendigo wendigo)
        {
            data = wendigo.Data;
            chronos = wendigo.Chronos;
        }

        public void Subscribe(Action<float> onVelocityUpdate) => this.onVelocityUpdate += onVelocityUpdate;

        public void MoveForward()
        {
            Accelerate();
            Decelerate();

            onVelocityUpdate?.Invoke(data.Movement.Velocity.magnitude);
        }

        public void Stop()
        {
            Decelerate();
            onVelocityUpdate?.Invoke(data.Movement.Velocity.magnitude);
        }

        private void Accelerate()
        {
            Vector3 acceleration = data.Transform.Forward * data.Movement.WalkSpeed * chronos.DeltaTime;
            data.Movement.Velocity += acceleration;
        }

        private void Decelerate()
        {
            float percent = data.Movement.Deceleration * chronos.DeltaTime;
            data.Movement.Velocity = Vector3.Lerp(data.Movement.Velocity, Vector3.zero, percent);
        }
    }
}
