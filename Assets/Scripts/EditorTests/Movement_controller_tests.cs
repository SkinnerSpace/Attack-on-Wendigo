using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Movement_controller_tests 
    {
        [Test]
        public void Moves()
        {
            ICharacterData data = new MockCharacterData() { Speed = 100f };
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            MovementController movementController = new MovementController(data, chronos);
            movementController.Move(Vector3.forward);

            Assert.AreEqual(new Vector3(0f,0f,100f), data.Velocity);
        }
    }
}

