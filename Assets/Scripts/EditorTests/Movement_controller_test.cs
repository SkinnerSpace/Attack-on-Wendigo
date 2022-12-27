using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Movement_controller_test
    {
        private Vector3 position = Vector3.zero;
        private Vector3 targetPos = new Vector3(3f, 0f, 3f);

        private const float speed = 10f;
        private const float rotationSpeed = 1f;
        private const float delta = 0.5f;

        [Test]
        public void Rotator_is_created()
        {
            Rotator rotator = new Rotator(Substitute.For<ITransformProxy>(), Substitute.For<IClock>(), rotationSpeed);
            Assert.That(rotator != null);
        }

        [Test]
        public void Rotator_calculate_direction_to_target_correctly()
        {
            /*
            ITransformProxy transform = new FakeTransformProxy(position);

            Rotator rotator = new Rotator(transform, Substitute.For<IClock>(), rotationSpeed);
            Vector3 direction = rotator.CalculateExpectedDirection(targetPos);

            Assert.AreEqual(1, Mathf.Round(direction.magnitude));
            */
        }

        [Test]
        public void Rotator_calculates_angle_to_target_correctly()
        {
            /*
            Rotator rotator = new Rotator(Substitute.For<ITransformProxy>(), Substitute.For<IClock>(), speed);
            Vector3 direction = rotator.CalculateExpectedDirection(targetPos);
            Vector3 angle = rotator.CalculateAngle(direction);

            Assert.AreEqual(new Vector3(0f, 45f, 0f), angle);
            */
        }

        [Test]
        public void Rotator_rotates_to_target_correctly()
        {
            ITransformProxy transform = new FakeTransformProxy(position);
            IClock clock = Substitute.For<IClock>();
            clock.Delta.Returns(delta);

            Rotator rotator = new Rotator(transform, clock, rotationSpeed);
            rotator.RotateTo(targetPos);

            Assert.AreEqual(new Vector3(0f, 22.5f, 0f), transform.Angle);
        }

        [Test]
        public void Mover_is_creared()
        {
            Mover mover = new Mover(Substitute.For<ITransformProxy>(), Substitute.For<IClock>(), speed);
            Assert.That(mover != null);
        }

        [Test]
        public void Mover_moves_forward()
        {
            ITransformProxy transform = new FakeTransformProxy(position);
            IClock clock = Substitute.For<IClock>();
            clock.Delta.Returns(delta);

            Mover mover = new Mover(transform, clock, speed);
            mover.MoveForward();

            Assert.AreEqual(new Vector3(0f, 0f, 5f), transform.Position);
        }

        [Test]
        public void Single_leg_is_created()
        {
            Leg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            Assert.That(leg != null);
        }

        [Test]
        public void Single_leg_is_equipped()
        {
            Leg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            leg.Equip(Substitute.For<ILegRaycaster>(), Substitute.For<ILegEngine>(), Substitute.For<ILegsSync>());

            Assert.That(leg.IsEquipped);
        }
    }
}
