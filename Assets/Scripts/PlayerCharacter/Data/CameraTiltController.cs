using UnityEngine;
using System;

namespace Character
{
    public class CameraTiltController : BaseController, IMovementReaderObserver
    {
        private ICharacterData data;
        private IChronos chronos;
        private IInputReader input;

        public override void Initialize(PlayerCharacter main)
        {
            data = main.OldData;
            chronos = main.Chronos;
            input = main.InputReader;
        }

        public override void Connect() => input.Get<MovementInputReader>().Subscribe(this);
        public override void Disconnect() => input.Get<MovementInputReader>().Unsubscribe(this);

        public void Move(Vector3 inDirection)
        {
            float tiltAngle = -1 * inDirection.x * data.TiltMaxAngle;
            data.CameraTiltEuler = Vector3.Lerp(data.CameraTiltEuler, new Vector3(0f, 0f, tiltAngle), data.TiltSpeed * chronos.DeltaTime);
        }
    }
}