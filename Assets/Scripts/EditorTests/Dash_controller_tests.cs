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
            ICharacterData data = Substitute.For<ICharacterData>();
            IChronos chronos = Substitute.For<IChronos>();
            IFunctionTimer timer = Substitute.For<IFunctionTimer>();

            DashController dashController = new DashController(data, chronos, timer);

            Assert.That(dashController != null);
        }

        [Test]
        public void Reached_the_expected_position()
        {
            float dashDistance = 10f;

            ICharacterData data = new MockCharacterData() { Deceleration = 1f, DashDistance = dashDistance };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(0.1f);
            IFunctionTimer timer = Substitute.For<IFunctionTimer>();

            DashController dashController = new DashController(data, chronos, timer);
            dashController.Move(Vector3.zero);
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

        [Test]
        public void Can_not_use_dash_more_than_once_without_taking_a_break()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            IChronos chronos = Substitute.For<IChronos>();
            IFunctionTimer timer = Substitute.For<IFunctionTimer>();

            DashController dashController = new DashController(data, chronos, timer);
            dashController.Dash();

            Assert.That(data.IsAbleToDash);
        }

        [Test]
        public void Dash_restores_after_cool_down_time()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            IChronos chronos = Substitute.For<IChronos>();
            MockFunctionTimer timer = new MockFunctionTimer();

            DashController dashController = new DashController(data, chronos, timer);
            dashController.Dash();

            timer.Set("CoolDown", data.DashCoolDownTime, dashController.CoolDown);
            timer.TimeOut();

            Assert.That(!data.IsAbleToDash);
        }

        [Test]
        public void Dash_forward()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            IChronos chronos = Substitute.For<IChronos>();
            MockFunctionTimer timer = new MockFunctionTimer();

            DashController dashController = new DashController(data, chronos, timer);
            dashController.Move(Vector3.forward);

            Assert.AreEqual(new Vector2(0f, 1f), data.DashDirection.Round(1));
        }

        [Test]
        public void Dash_right()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            IChronos chronos = Substitute.For<IChronos>();
            MockFunctionTimer timer = new MockFunctionTimer();

            DashController dashController = new DashController(data, chronos, timer);
            dashController.Move(Vector3.right);

            Assert.AreEqual(new Vector2(1f, 0f), data.DashDirection.Round(1));
        }

        [Test]
        public void Dash_backward()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            IChronos chronos = Substitute.For<IChronos>();
            MockFunctionTimer timer = new MockFunctionTimer();

            DashController dashController = new DashController(data, chronos, timer);
            dashController.Move(Vector3.back);

            Assert.AreEqual(new Vector2(0f, -1f), data.DashDirection.Round(1));
        }

        [Test]
        public void Dash_left()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            IChronos chronos = Substitute.For<IChronos>();
            MockFunctionTimer timer = new MockFunctionTimer();

            DashController dashController = new DashController(data, chronos, timer);
            dashController.Move(Vector3.left);

            Assert.AreEqual(new Vector2(-1f, 0f), data.DashDirection.Round(1));
        }
    }
}
