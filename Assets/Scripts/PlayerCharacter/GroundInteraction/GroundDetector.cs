using System;
using UnityEngine;

namespace Character
{
    public class GroundDetector : BaseController, IMoverObserver
    {
        private PlayerCharacter main;
        private CharacterMover mover;
        private ICharacterData data;
        private IGroundDetector detector;

        private event Action notifyOnGrounded;

        public void Subscribe(IGroundObserver observer) => notifyOnGrounded += observer.Land;
        public void Unsubscribe(IGroundObserver observer) => notifyOnGrounded -= observer.Land;

        public override void Initialize(PlayerCharacter main)
        {
            this.main = main;

            mover = main.Mover;
            data = main.OldData;
            detector = new GroundCollisionDetector();
        }

        public override void Connect() => main.Mover.Subscribe(this);
        public override void Disconnect()
        {
            //notifyOnGrounded += () => mover.DisableController();
        }

        public void Update()
        {
            Vector3 detectionPosition = GetDetectionPosition(data.Position, data.GroundDetectionHeight);

            data.WasGrounded = data.IsGrounded;
            data.IsGrounded = detector.Check(detectionPosition, data.GroundDetectionRadius);
            NotifyOnChange();
        }

        public static Vector3 GetDetectionPosition(Vector3 position, float height) => position - new Vector3(0f, height, 0f);

        private void NotifyOnChange()
        {
            if (data.WasGrounded != data.IsGrounded && data.IsGrounded)
                notifyOnGrounded?.Invoke();
        }
    }
}