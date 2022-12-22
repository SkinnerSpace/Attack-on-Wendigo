using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Movement_controller_test
    {
        [Test]
        public void Movement_controller_has_unity_service()
        {
            ITransformProxy transform = Substitute.For<ITransformProxy>();
            IMovementController movementController = new MovementController(transform);

            Assert.That(movementController.UnityService != null);
        }

        [Test]
        public void Movement_controller_has_legs_synchronizer()
        {
            ITransformProxy transform = Substitute.For<ITransformProxy>();
            IMovementController movementController = new MovementController(transform);
            AddFakeLegs(1, movementController);

            Assert.That(movementController.LegsSync != null);
        }

        [Test]
        public void Movement_controller_has_at_least_one_leg()
        {
            ITransformProxy transform = Substitute.For<ITransformProxy>();
            IMovementController movementController = new MovementController(transform);
            AddFakeLegs(1, movementController);

            Assert.That(movementController.LegsSync.GetLegsCount() > 0);
        }

        [Test]
        public void Movement_controller_has_two_legs()
        {
            ITransformProxy transform = Substitute.For<ITransformProxy>();
            IMovementController movementController = new MovementController(transform);
            AddFakeLegs(2, movementController);

            Assert.That(movementController.LegsSync.GetLegsCount() == 2);
        }

        [Test]
        public void Movement_controller_has_no_more_than_two_legs()
        {
            ITransformProxy transform = Substitute.For<ITransformProxy>();
            IMovementController movementController = new MovementController(transform);
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

        [Test]
        public void Move_test()
        {
            Vector3 originalPosition = Vector3.zero;
            float speed = 3f;
            float delta = 1f;

            Vector3 expectedPosition = originalPosition + ((Vector3.forward * speed) * delta);

            ITransformProxy transformProxy = new FakeTransformProxy(originalPosition);
            IUnityService UnityService = Substitute.For<IUnityService>();
            UnityService.Delta.Returns(delta);

            MovementController movementController = new MovementController(transformProxy);
            movementController.SetUnityService(UnityService);

            movementController.MoveBodyForward(speed);

            Assert.AreEqual(expectedPosition, transformProxy.Position);
        }
    }
}
