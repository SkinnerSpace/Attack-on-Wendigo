﻿using UnityEngine;
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

        private event Action onDash;

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
            float dashAngle = 0f;

            if (direction.x > 0f) dashAngle = 90f;
            if (direction.x < 0f) dashAngle = 270f;
            if (direction.z < 0f) dashAngle = 180f;
            if (direction.z > 0f) dashAngle = 0f;

            float rads = (data.Euler.y + dashAngle) * Mathf.Deg2Rad;
            data.DashDirection = new Vector2(Mathf.Sin(rads), Mathf.Cos(rads));
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

        public void CoolDown() => data.IsAbleToDash = false;
    }
}