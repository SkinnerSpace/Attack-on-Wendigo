﻿using UnityEngine;

namespace Character
{
    public class DecelerationController : BaseController, IMoverObserver
    {
        private PlayerCharacter main;
        private ICharacterData data;
        private IChronos chronos;

        public override void Initialize(PlayerCharacter main)
        {
            this.main = main;
            data = main.OldData;
            chronos = main.Chronos;
        }

        public override void Connect() => main.Mover.Subscribe(this);
        public override void Disconnect() => main.Mover.Unsubscribe(this);

        public void Update()
        {
            SetDeceleration();
            ApplyDeceleration();
        }

        public void SetDeceleration() => data.Deceleration = data.IsGrounded ? data.GroundDeceleration : data.AirDeceleration;

        public void ApplyDeceleration()
        {
            data.FlatVelocity = Vector2.Lerp(data.FlatVelocity, Vector2.zero, data.Deceleration * chronos.DeltaTime);
        }
    }
}