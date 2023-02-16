using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Camera_tilt_controller_tests
    {
        [Test]
        public void Camera_stands_still_when_no_movement()
        {
            ICharacterData data = new MockCharacterData();
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);
            CameraTiltController tiltController = new CameraTiltController(data, chronos);

            tiltController.Move(Vector3.zero);

            Assert.That(data.CameraTiltEuler.z == 0f);
        }

        [Test]
        public void Camera_is_tilting_right_when_move_right()
        {
            ICharacterData data = new MockCharacterData();
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);
            CameraTiltController tiltController = new CameraTiltController(data, chronos);

            tiltController.Move(Vector3.right);

            Assert.That(data.CameraTiltEuler.z < 0f);
        }

        [Test]
        public void Camera_is_tilting_left_when_move_left()
        {
            ICharacterData data = new MockCharacterData();
            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);
            CameraTiltController tiltController = new CameraTiltController(data, chronos);

            tiltController.Move(Vector3.left);

            Assert.That(data.CameraTiltEuler.z > 0f);
        }
    }
}
