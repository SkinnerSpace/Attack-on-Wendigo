using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Movement_controller_tests 
    {
#pragma warning disable CS1701

        [Test]
        public void Velocity_is_increasing()
        {
            ICharacterData data = new MockCharacterData() { Speed = 100f };
            MovementController movementController = CreateMovementController(data);
            movementController.Move(Vector3.forward);

            Assert.AreNotEqual(Vector3.zero, data.FlatVelocity);
        }

        [Test]
        public void Moves_forward()
        {
            ICharacterData data = new MockCharacterData() { Speed = 100f };
            MovementController movementController = CreateMovementController(data);
            movementController.Move(Vector3.forward);

            Assert.That(data.FlatVelocity.y > 0f);
        }

        [Test]
        public void Moves_backward()
        {
            ICharacterData data = new MockCharacterData() { Speed = 100f };
            MovementController movementController = CreateMovementController(data);
            movementController.Move(Vector3.back);

            Assert.That(data.FlatVelocity.y < 0f);
        }

        [Test]
        public void Moves_right()
        {
            ICharacterData data = new MockCharacterData() { Speed = 100f };
            MovementController movementController = CreateMovementController(data);
            movementController.Move(Vector3.right);

            Assert.That(data.FlatVelocity.x > 0f);
        }

        [Test]
        public void Moves_left()
        {
            ICharacterData data = new MockCharacterData() { Speed = 100f };
            MovementController movementController = CreateMovementController(data);
            movementController.Move(Vector3.left);

            Assert.That(data.FlatVelocity.x < 0f);
        }

        private MovementController CreateMovementController(ICharacterData data)
        {
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            IInputReader inputReader = Substitute.For<IInputReader>();
            MovementController movementController = new MovementController();
            movementController.Initialize(data, chronos, inputReader);

            return movementController;
        }
    }
}

