using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Damped_spring_tests
    {
        [Test]
        public void Amplitude_grows_when_grounded()
        {
            ICharacterData data = new MockCharacterData() { 
                PreviousVerticalVelocity = 10f, 
                JumpHeight = 10f, 
                MaxJumpCount = 2, 
                Gravity = 26, 
                DampedSpring = new MockDampedSpringData() { Power = 10f, Time = 0.5f }
            };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            DampedSpring dampedSpring = new DampedSpring();
            dampedSpring.Initialize(data, chronos);

            dampedSpring.Land();
            Assert.AreEqual(1.8f, data.DampedSpring.Amplitude.Round(1));
        }

        [Test]
        public void Position_updates_when_current_time_less_than_max()
        {
            ICharacterData data = new MockCharacterData()
            {
                PreviousVerticalVelocity = 10f,
                JumpHeight = 10f,
                MaxJumpCount = 2,
                Gravity = 26,
                DampedSpring = new MockDampedSpringData() { Power = 10f, Time = 0.5f }
            };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(0.1f);

            DampedSpring dampedSpring = new DampedSpring();
            dampedSpring.Initialize(data, chronos);

            dampedSpring.Land();
            dampedSpring.Update();
            Assert.AreNotEqual(Vector3.zero, data.CameraLocalPos);
        }

        [Test]
        public void Motion_stops_on_time_out()
        {
            ICharacterData data = new MockCharacterData()
            {
                PreviousVerticalVelocity = 10f,
                JumpHeight = 10f,
                MaxJumpCount = 2,
                Gravity = 26,
                DampedSpring = new MockDampedSpringData() { Power = 10f, Time = 0.5f }
            };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            DampedSpring dampedSpring = new DampedSpring();
            dampedSpring.Initialize(data, chronos);

            dampedSpring.Land();
            dampedSpring.Update();
            Assert.AreEqual(Vector3.zero, data.CameraLocalPos.Round(1));
        }
    }
}
