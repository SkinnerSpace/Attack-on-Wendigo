using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Gravity_controller_tests
    {
        [Test]
        public void Gravity_is_being_applied()
        {
            ICharacterData data = new MockCharacterData() { VerticalVelocity = 100f, Gravity = 200f };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);
            GravityController gravityController = new GravityController(data, chronos);

            gravityController.ApplyGravity();

            Assert.That(data.VerticalVelocity < 0f);
        }

        [Test]
        public void Gravity_is_set_to_zero_when_grounded()
        {
            ICharacterData data = new MockCharacterData() { VerticalVelocity = 100f, Gravity = 200f };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);
            GravityController gravityController = new GravityController(data, chronos);

            data.IsGrounded = true;
            gravityController.OnGrounded();
            gravityController.ApplyGravity();

            Assert.AreEqual(0f, data.VerticalVelocity);
        }
    }
}
