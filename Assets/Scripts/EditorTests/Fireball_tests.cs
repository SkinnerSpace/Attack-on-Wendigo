using System;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using Moq;

namespace Tests
{
    public class Fireball_tests
    {
#pragma warning disable CS1701

        [Test]
        public void Fireball_moves()
        {
/*            float speed = 20f;

            IFireballData data = Substitute.For<IFireballData>();
            data.Speed.Returns(speed);
            data.Forward.Returns(Vector3.forward);

            IChronos chronos = Substitute.For<IChronos>();
            chronos.DeltaTime.Returns(1f);

            FireballMover mover = new FireballMover(data, chronos);
            mover.Move();

            Vector3 destination = Vector3.forward * speed;
            Assert.AreEqual(destination, data.Position);*/
        }
    }
}
