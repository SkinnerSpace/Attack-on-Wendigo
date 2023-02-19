using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Jump_controller_tests
    {
        [Test]
        public void Is_not_able_to_jump_when_not_grounded()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = false };

            IJumpController jumpController = new JumpController();
            jumpController.SetData(data);

            jumpController.TryToJump();

            Assert.AreEqual(0, data.JumpCount);
        }

        [Test]
        public void Is_able_to_jump_when_grounded()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = true };

            IJumpController jumpController = new JumpController();
            jumpController.SetData(data);

            jumpController.TryToJump();

            Assert.AreEqual(1, data.JumpCount);
        }

        [Test]
        public void Vertical_velocity_increased()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = true, JumpHeight = 10f, Gravity = 26f, MaxJumpCount = 2 };

            IJumpController jumpController = new JumpController();
            jumpController.SetData(data);

            jumpController.TryToJump();

            Assert.Greater(data.VerticalVelocity, 0f);
        }

        [Test]
        public void Jump_in_the_air()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = true, JumpHeight = 10f, MaxJumpCount = 2 };

            IJumpController jumpController = new JumpController();
            jumpController.SetData(data);

            jumpController.TryToJump();

            data.IsGrounded = false;
            jumpController.Stop();

            jumpController.TryToJump();

            Assert.AreEqual(2, data.JumpCount);
        }

        [Test]
        public void Is_not_able_to_jump_in_the_air_when_grounded()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = true, JumpHeight = 10f, MaxJumpCount = 2 };

            IJumpController jumpController = new JumpController();
            jumpController.SetData(data);

            jumpController.TryToJump();
            jumpController.Stop();
            jumpController.TryToJump();

            Assert.Less(data.JumpCount, 2);
        }

        [Test]
        public void Jump_no_more_than_allowed()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = true, JumpHeight = 10f, MaxJumpCount = 2 };

            IJumpController jumpController = new JumpController();
            jumpController.SetData(data);

            jumpController.TryToJump();

            data.IsGrounded = false;
            jumpController.Stop();

            jumpController.TryToJump();
            jumpController.Stop();

            jumpController.TryToJump();

            Assert.AreEqual(2, data.JumpCount);
        }

        [Test]
        public void Jump_is_reset()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = true };

            IJumpController jumpController = new JumpController();
            jumpController.SetData(data);

            jumpController.TryToJump();
            jumpController.Stop();
            jumpController.Land();

            Assert.AreEqual(0, data.JumpCount);
        }
    }
}
