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
            /*JumpController jumpController = new JumpController(data);

            jumpController.OnJump();*/

            Assert.AreEqual(0, data.JumpCount);
        }

        [Test]
        public void Is_able_to_jump_when_grounded()
        {
            ICharacterData data = new MockCharacterData() { IsGrounded = true };
          /*  JumpController jumpController = new JumpController(data);

            jumpController.OnJump();*/

            Assert.AreEqual(1, data.JumpCount);
        }

        [Test]
        public void Vertical_velocity_increased()
        {
            ICharacterData data = new MockCharacterData() { JumpHeight = 10f, Gravity = 10f, IsGrounded = true };
   /*         JumpController jumpController = new JumpController(data);

            jumpController.OnJump();
*/
            Assert.That(data.VerticalVelocity > 0);
        }

        [Test]
        public void Jump_in_the_air()
        {
            ICharacterData data = new MockCharacterData() { MaxJumpCount = 2, IsGrounded = true };
 /*           JumpController jumpController = new JumpController(data);*/

            /*jumpController.OnJump();
            data.IsGrounded = false;
            jumpController.OnStop();

            jumpController.OnJump();*/

            Assert.AreEqual(data.MaxJumpCount, data.JumpCount);
        }

        [Test]
        public void Is_not_able_to_jump_in_the_air_when_grounded()
        {
            ICharacterData data = new MockCharacterData() { MaxJumpCount = 2, IsGrounded = true };
          /*  JumpController jumpController = new JumpController(data);

            jumpController.OnJump();
            jumpController.OnJump();*/

            Assert.AreEqual(1, data.JumpCount);
        }

        [Test]
        public void Jump_no_more_than_allowed()
        {
            ICharacterData data = new MockCharacterData() { MaxJumpCount = 2, IsGrounded = true };
/*            JumpController jumpController = new JumpController(data);

            jumpController.OnJump();
            data.IsGrounded = false;
            jumpController.OnStop();

            jumpController.OnJump();
            jumpController.OnStop();

            jumpController.OnJump();*/

            Assert.AreEqual(data.MaxJumpCount, data.JumpCount);
        }

        [Test]
        public void Jump_is_reset()
        {
            ICharacterData data = new MockCharacterData();
           /* JumpController jumpController = new JumpController(data);

            jumpController.OnJump();
            jumpController.OnJump();
            jumpController.OnGrounded();*/

            Assert.AreEqual(0, data.JumpCount);
        }
    }
}
