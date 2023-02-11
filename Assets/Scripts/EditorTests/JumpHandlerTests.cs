using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class JumpHandlerTests
    {
        [Test]
        public void Jump_handler_exist()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            JumpController jumpController = new JumpController(data);

            Assert.That(jumpController.data != null);
        }

        [Test]
        public void Jump_count_increased()
        {
            ICharacterData data = new MockCharacterData();
            JumpController jumpController = new JumpController(data);

            jumpController.Jump();

            Assert.That(data.JumpCount > 0);
        }

        [Test]
        public void Vertical_velocity_increased()
        {
            ICharacterData data = new MockCharacterData();
            JumpController jumpController = new JumpController(data);

            jumpController.Jump();

            Assert.That(data.VerticalVelocity > 0);
        }

        [Test]
        public void Jumps_no_more_than_twice()
        {
            ICharacterData data = new MockCharacterData();
            JumpController jumpController = new JumpController(data);

            jumpController.Jump();
            jumpController.Jump();
            jumpController.Jump();

            Assert.AreEqual(data.MaxJumpCount, data.JumpCount);
        }

        [Test]
        public void Jump_cant_has_been_reset()
        {
            ICharacterData data = new MockCharacterData();
            JumpController jumpController = new JumpController(data);

            jumpController.Jump();
            jumpController.Jump();
            jumpController.ResetJumpCount();

            Assert.AreEqual(0, data.JumpCount);
        }
    }
}
