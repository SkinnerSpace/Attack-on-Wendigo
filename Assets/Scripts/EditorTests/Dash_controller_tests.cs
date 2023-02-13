using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Dash_controller_tests
    {
        [Test]
        public void Dash_controller_exists()
        {
            ICharacterData data = new MockCharacterData();
            IChronos chronos = Substitute.For<IChronos>();
            DashController dashController = new DashController(data, chronos);

            Assert.That(dashController != null);
        }

        [Test]
        public void Reached_the_expected_position()
        {
            float dashDistance = 10f;

            ICharacterData data = new MockCharacterData() { Deceleration = 1f, DashDistance = dashDistance };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(0.1f);

            DashController dashController = new DashController(data, chronos);
            dashController.Dash();

            DecelerationController decelerator = new DecelerationController(data, chronos);

            Vector3 positionOverTime = GetPositionOverTime(data, decelerator, chronos, 50).Round(10);

            Vector3 expectedPosition = data.Forward * dashDistance;
            Assert.AreEqual(expectedPosition, positionOverTime);
        }

        private Vector3 GetPositionOverTime(ICharacterData data, DecelerationController decelerator, IChronos chronos, int cycles)
        {
            for (int i = 0; i < cycles; i++)
            {
                data.Position += data.Velocity * chronos.DeltaTime;
                decelerator.ApplyDeceleration();
            }

            return data.Position;
        }
    }
}
