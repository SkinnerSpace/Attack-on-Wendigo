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
            ICharacterData data = new MockCharacterData() { VerticalVelocity = 10f, JumpHeight = 10f, MaxJumpCount = 2, Gravity = 26, DampedSpringPower = 1f };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            /*DampedSpring dampedSpring = new DampedSpring(data, chronos);
            dampedSpring.OnGrounded();

            float amplitude = data.DampedSpringAmplitude.Round(1);
            Assert.AreEqual(0.3f, amplitude);*/
        }

        [Test]
        public void Position_updates_when_current_time_less_than_max()
        {
            ICharacterData data = new MockCharacterData() { 
                VerticalVelocity = 10f, 
                JumpHeight = 10f, 
                MaxJumpCount = 2, 
                Gravity = 26,
                DampedSpringPower = 1f, 
                DampedSpringTime = 1f 
            };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            /*DampedSpring dampedSpring = new DampedSpring(data, chronos);
            dampedSpring.OnGrounded();
            dampedSpring.Update();*/

            Assert.AreNotEqual(Vector3.zero, data.CameraLocalPos);
        }

        [Test]
        public void Motion_stops_on_time_out()
        {
            ICharacterData data = new MockCharacterData()
            {
                VerticalVelocity = 10f,
                JumpHeight = 10f,
                MaxJumpCount = 2,
                Gravity = 26,
                DampedSpringPower = 1f,
                DampedSpringTime = 1f
            };

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            /*DampedSpring dampedSpring = new DampedSpring(data, chronos);
            dampedSpring.OnGrounded();
            data.CurrentDampedSpringTime = data.DampedSpringTime;
            dampedSpring.Update();*/

            Assert.AreEqual(Vector3.zero, data.CameraLocalPos);
        }
    }
}
