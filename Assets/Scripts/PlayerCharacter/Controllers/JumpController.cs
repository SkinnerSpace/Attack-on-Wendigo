using UnityEngine;

namespace Character
{
    public class JumpController : BaseController, IJumpController, IGroundObserver
    {
        private PlayerCharacter main;
        private ICharacterData data;
        private IInputReader input;

        public override void Initialize(PlayerCharacter main)
        {
            this.main = main;
            data = main.OldData;
            input = main.InputReader;
        }

        public void SetData(ICharacterData data) => this.data = data;

        public override void Connect()
        {
            main.GetController<GroundDetector>().Subscribe(this);
            input.Get<JumpInputReader>().Subscribe(this);
        }

        public override void Disconnect()
        {
            main.GetController<GroundDetector>().Unsubscribe(this);
            input.Get<JumpInputReader>().Unsubscribe(this);
        }

        public void TryToJump()
        {
            JumpOffTheGround();
            JumpInTheAir();
        }

        public void Stop() => data.IsJumping = false;

        public void JumpOffTheGround()
        {
            if (data.IsGrounded && data.JumpCount == 0)
            {
                data.IsJumping = true;
                ApplyJumpForce();
            }
        }

        public void JumpInTheAir()
        {
            if (!data.IsJumping && (!data.IsGrounded && data.JumpCount > 0) && (data.JumpCount < data.MaxJumpCount))
            {
                data.IsJumping = true;
                ApplyJumpForce();
            }
        }

        public void ApplyJumpForce()
        {
            data.JumpCount += 1;
            data.VerticalVelocity = GetJumpVelocity() * data.JumpCount;
        }

        private float GetJumpVelocity() => Mathf.Sqrt(data.JumpHeight * 2f * data.Gravity);

        public void Land() => data.JumpCount = 0;
    }
}