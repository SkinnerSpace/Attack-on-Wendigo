using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class Titan_creation_test
    {
        [Test]
        public void Titan_is_created()
        {
            Titan titan = TitansFactory.CreateTitan(TitanTypes.GINGERBREAD);
            Assert.That(titan != null);
        }

        [Test]
        public void Titan_has_data()
        {
            Titan titan = TitansFactory.CreateTitan(TitanTypes.GINGERBREAD);
            ITitanData data = Substitute.For<ITitanData>();
            titan.SetData(data);

            Assert.That(titan.data != null);
        }

        [Test]
        public void Data_is_valid()
        {
            string name = "Kevin";
            float speed = 3f;

            Titan titan = TitansFactory.CreateTitan(TitanTypes.GINGERBREAD);

            FakeTitanData data = new FakeTitanData
            {
                name = name,
                speed = speed
            };

            titan.SetData(data);

            Assert.That(titan.data.Name == name);
            Assert.That(titan.data.Speed == speed);
        }

        [Test]
        public void Titan_has_transform()
        {
            Vector3 originalPosition = Vector3.zero;
            Titan titan = TitansFactory.CreateTitan(TitanTypes.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            titan.SetTransform(transform);

            Assert.That(titan.transform.Position == originalPosition);
        }

        [Test]
        public void Titan_has_movement_controller()
        {
            Titan titan = TitansFactory.CreateTitan(TitanTypes.GINGERBREAD);

            IMovementController movementController = Substitute.For<IMovementController>();
            titan.SetMovementController(movementController);

            Assert.That(titan.movementController != null);
        }

        [Test]
        public void Titan_moves_when_update_is_called()
        {
            float speed = 1f;

            Titan titan = TitansFactory.CreateTitan(TitanTypes.GINGERBREAD);
            titan.SetTransform(Substitute.For<ITransformProxy>());

            FakeTitanData data = new FakeTitanData
            {
                speed = speed
            };
            titan.SetData(data);

            IMovementController movementController = Substitute.For<IMovementController>();
            titan.SetMovementController(movementController);

            titan.Update();

            movementController.Received().Move(speed);
        }
    }
}
