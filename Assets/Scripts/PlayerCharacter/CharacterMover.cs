﻿using UnityEngine;
using System;

namespace Character
{
    public class CharacterMover : MonoBehaviour
    {
        private CharacterData data;

        private event Action onUpdate;
        private bool isReady;

        private void Start()
        {
            HelicopterEvents.current.onIsGoingToSetOff += ResetState;
            PlayerEvents.current.onForcedMoverReadinessUpdate += (bool isReady) => this.isReady = isReady;
        }

        public void Initialize(PlayerCharacter main) => data = main.OldData;

        public void Connect() => isReady = true;

        public void Disconnect() { }

        public void Subscribe(IMoverObserver observer) => onUpdate += observer.Update;
        public void Unsubscribe(IMoverObserver observer) => onUpdate -= observer.Update;

        private void Update()
        {
            if (isReady)
            {
                data.CameraRotation = Quaternion.Euler(data.CameraViewEuler + data.CameraTiltEuler + data.ShakeEuler);
                data.CameraLocalPos = data.CameraDampedPos + data.ShakePosition;
            }
        }

        private void FixedUpdate()
        {
            if (isReady)
            {
                data.PreviousVerticalVelocity = data.VerticalVelocity;
                data.Velocity = new Vector3(data.FlatVelocity.x, data.VerticalVelocity, data.FlatVelocity.y) + data.ExternalForce;

                data.Controller.Move(data.Velocity * Time.fixedDeltaTime);

                onUpdate?.Invoke();
            }
        }

        private void ResetState()
        {
            isReady = false;
        }

        public void DisableController()
        {
            isReady = false;
            data.Controller.enabled = false;
        }
    }
}