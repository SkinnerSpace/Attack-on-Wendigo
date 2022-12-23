using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Movement_controller_test
    {
        [Test]
        public void Moves()
        {
            Vector3 originalPosition = Vector3.zero;
            float speed = 3f;
            float delta = 1f;

            Vector3 expectedPosition = originalPosition + ((Vector3.forward * speed) * delta);

            ITransformProxy transformProxy = new FakeTransformProxy(originalPosition);
            IClock clock = Substitute.For<IClock>();
            clock.Delta.Returns(delta);

            IMovementController movementController = new MovementController(transformProxy, clock);
            movementController.Move(speed);

            Assert.AreEqual(expectedPosition, transformProxy.Position);
        }

        [Test]
        public void legs_sync_was_added_after_adding_a_leg()
        {
            IMovementController movementController = new MovementController(Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            AddFakeLegs(1, movementController);

            Assert.That(movementController.LegsSync != null);
        }

        [Test]
        public void Torso_is_connected()
        {
            IMovementController movementController = new MovementController(Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            ITorso torso = Substitute.For<ITorso>();
            movementController.AddTorso(torso);
            movementController.Move(1f);

            torso.Received().Update();
        }

        [Test]
        public void Torso_modifier_is_zero_without_legs_sync()
        {
            ITorsoController movementController = new MovementController(Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            Assert.AreEqual(0f, movementController.GetTorsoModifier());
        }

        [Test]
        public void Torso_modifier_is_none_zero_with_legs_sync()
        {
            MovementController movementController = new MovementController(Substitute.For<ITransformProxy>(), Substitute.For<IClock>());

            ITransformProxy transformProxy = new FakeTransformProxy(Vector3.one);
            ILeg leg = new Leg(Sides.Right, transformProxy);

            movementController.AddLeg(leg);

            Assert.AreEqual(1f, movementController.GetTorsoModifier());
        }

        [Test]
        public void Movement_controller_has_at_least_one_leg()
        {
            IMovementController movementController = new MovementController(Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            AddFakeLegs(1, movementController);

            Assert.That(movementController.LegsSync.GetLegsCount() > 0);
        }

        [Test]
        public void Movement_controller_has_two_legs()
        {
            IMovementController movementController = new MovementController(Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            AddFakeLegs(2, movementController);

            Assert.That(movementController.LegsSync.GetLegsCount() == 2);
        }

        [Test]
        public void Movement_controller_has_no_more_than_two_legs()
        {
            IMovementController movementController = new MovementController(Substitute.For<ITransformProxy>(), Substitute.For<IClock>());
            AddFakeLegs(3, movementController);

            Assert.That(movementController.LegsSync.GetLegsCount() <= 2);
        }

        private void AddFakeLegs(int count, IMovementController movementController)
        {
            for (int i = 0; i < count; i++)
            {
                ILeg leg = Substitute.For<ILeg>();
                movementController.AddLeg(leg);
            }
        } 
    }
}
