using UnityEngine;
using System;

namespace Character
{
    public class DashController : BaseController, IMovementReaderObserver
    {
        private const string COOL_DOWN = "CoolDown";

        private ICharacterData data;
        private IChronos chronos;
        private IFunctionTimer timer;
        private IInputReader input;

        public event Action onDash;
        public event Action<float, DashDirections> onDashAngle;
        public event Action onStop;

        private float rawDashAngle;
        private float dashAngleDeg;
        private float dashAngleRad;

        private DashDirections dashDirection;

        public override void Initialize(PlayerCharacter main)
        {
            data = main.OldData;
            chronos = main.Chronos;
            timer = main.Timer;
            input = main.InputReader;
        }

        public override void Connect()
        {
            input.Get<MovementInputReader>().Subscribe(this);
            input.Get<DashInputReader>().Subscribe(Dash);
        }

        public override void Disconnect()
        {
            input.Get<MovementInputReader>().Unsubscribe(this);
            input.Get<DashInputReader>().Unsubscribe(Dash);
        }

        public void Subscribe(Action onDash) => this.onDash += onDash;
        public void Unsubscribe(Action onDash) => this.onDash -= onDash;

        public void Move(Vector3 direction)
        {
            rawDashAngle = GetAngleInOneOfTheFourDirections(direction);
            dashAngleDeg = data.Euler.y + rawDashAngle;
            dashAngleRad = dashAngleDeg * Mathf.Deg2Rad;
            data.DashDirection = new Vector2(Mathf.Sin(dashAngleRad), Mathf.Cos(dashAngleRad));
        }

        private float GetAngleInOneOfTheFourDirections(Vector3 direction)
        {
            if (direction.x > 0f){
                dashDirection = DashDirections.Right;
                return 90f;
            }

            if (direction.x < 0f){
                dashDirection = DashDirections.Left;
                return 270f;
            }

            if (direction.z < 0f){
                dashDirection = DashDirections.Backward;
                return 180f;
            }

            if (direction.z > 0f){
                dashDirection = DashDirections.Forward;
                return 0f;
            }

            dashDirection = DashDirections.Forward;
            return 0f;
        }

        public void Dash()
        {
            if (!data.IsAbleToDash)
            {
                data.IsAbleToDash = true;
                Vector2 dashVelocity = data.DashDirection * GetDashPower();
                data.FlatVelocity += dashVelocity;

                timer.Set(COOL_DOWN, data.DashCoolDownTime, CoolDown);
                onDash?.Invoke();
                onDashAngle?.Invoke(dashAngleDeg, dashDirection);

                PlayerEvents.current.NotifyOnDash();
            }
        }

        public float GetDashPower()
        {
            float dragAdjustment = data.Deceleration * chronos.DeltaTime + 1f;
            float distanceAdjustment = data.Deceleration * 0.1f;
            float resistance = Mathf.Log(1f / dragAdjustment) / chronos.DeltaTime.Negative();

            float dashPower = (data.DashDistance + distanceAdjustment) * resistance;
            return dashPower;
        }

        public void CoolDown()
        {
            data.IsAbleToDash = false;
            onStop?.Invoke();
        }
    }
}