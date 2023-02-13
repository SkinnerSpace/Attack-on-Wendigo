using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class Deceleration_controller_tests
    {
        [Test]
        public void Deceleration_controller_exists()
        {
            ICharacterData data = new MockCharacterData();
            IChronos chronos = Substitute.For<IChronos>();
            DecelerationController decelerator = new DecelerationController(data, chronos);

            Assert.That(decelerator != null);
        }

        [Test]
        public void Deceleration_is_set_to_ground_when_grounded()
        {
            ICharacterData data = new MockCharacterData() { GroundDeceleration = 10f, IsGrounded = true };
            IChronos chronos = Substitute.For<IChronos>();
            DecelerationController decelerator = new DecelerationController(data, chronos);
            decelerator.SetDeceleration();

            Assert.AreEqual(data.GroundDeceleration, data.Deceleration);
        }

        [Test]
        public void Deceleration_is_set_to_air_when_not_grounded()
        {
            ICharacterData data = new MockCharacterData() { AirDeceleration = 5f, IsGrounded = false };
            IChronos chronos = Substitute.For<IChronos>();
            DecelerationController decelerator = new DecelerationController(data, chronos);
            decelerator.SetDeceleration();

            Assert.AreEqual(data.AirDeceleration, data.Deceleration);
        }

        [Test]
        public void Deceleration_does_not_change_when_being_applied()
        {
            float originalDeceleration = 10f;

            ICharacterData data = new MockCharacterData() { Deceleration = originalDeceleration };
            IChronos chronos = Substitute.For<IChronos>();
            DecelerationController decelerator = new DecelerationController(data, chronos);
            decelerator.ApplyDeceleration();

            Assert.AreEqual(originalDeceleration, data.Deceleration);
        }

        [Test]
        public void Deceleration_is_being_applied()
        {
            Vector2 originalFlatVelocity = Vector2.one;

            ICharacterData data = new MockCharacterData() { FlatVelocity = originalFlatVelocity, GroundDeceleration = 10f, IsGrounded = true };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            DecelerationController decelerator = new DecelerationController(data, chronos);
            decelerator.Decelerate();

            Assert.That(data.FlatVelocity.magnitude < originalFlatVelocity.magnitude);
        }
    }
}
