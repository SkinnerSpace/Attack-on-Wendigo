using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class titan_creation_test
    {
        private Vector3 originalPosition = Vector3.zero;

        [Test]
        public void data_is_valid()
        {
            string name = "Kevin";
            float speed = 3f;

            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            TitanData data = new TitanData();
            data.name = name;
            data.speed = speed;

            titanCharacter.SetData(data);

            Assert.That(titanCharacter.data.name == name && titanCharacter.data.speed == speed);
        }

        [Test]
        public void position_is_correct()
        {
            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            titanCharacter.SetTransform(transform);

            Assert.That(titanCharacter.transform.Position == originalPosition);
        }

        [Test]
        public void unity_service_exists()
        {
            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            IMovementController movementController = new MovementController(transform);

            titanCharacter.SetTransform(transform);
            titanCharacter.SetMovementController(movementController);

            Assert.That(titanCharacter.movementController.unityService != null);
        }

        [Test]
        public void both_legs_exist()
        {
            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            IMovementController movementController = new MovementController(transform);

            AddLegs(2, movementController);

            titanCharacter.SetTransform(transform);
            titanCharacter.SetMovementController(movementController);

            Assert.True(titanCharacter.movementController.legsSync.GetLegsCount() == 2);
        }

        [Test]
        public void no_more_than_two_legs_exist()
        {
            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            IMovementController movementController = new MovementController(transform);

            AddLegs(3, movementController);

            titanCharacter.SetTransform(transform);
            titanCharacter.SetMovementController(movementController);

            Assert.True(titanCharacter.movementController.legsSync.GetLegsCount() <= 2);
        }

        [Test]
        public void leg_exist()
        {
            Vector3 legPos = new Vector3(-1f, 0f, 0f);

            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            IMovementController movementController = new MovementController(transform);

            Leg leg = CreateLeg(Sides.Left, legPos, transform);
            movementController.AddLeg(leg);

            titanCharacter.SetTransform(transform);
            titanCharacter.SetMovementController(movementController);

            Assert.That(titanCharacter.movementController.legsSync.GetLegsCount() > 0);
        }

        [Test]
        public void leg_has_synchronizer()
        {
            Vector3 legPos = new Vector3(-1f, 0f, 0f);

            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            IMovementController movementController = new MovementController(transform);

            Leg leg = CreateLeg(Sides.Left, legPos, transform);
            movementController.AddLeg(leg);

            titanCharacter.SetTransform(transform);
            titanCharacter.SetMovementController(movementController);

            titanCharacter.Move();

            Assert.That(leg.synchronizer != null);
        }

        [Test]
        public void legs_move()
        {
            Vector3 leftLegPos = new Vector3(-1f, 0f, 0f);
            Vector3 rightLegPos = new Vector3(1f, 0f, 0f);

            Titan titanCharacter = TitansFactory.CreateTitan(Titans.GINGERBREAD);

            ITransformProxy transform = new FakeTransformProxy(originalPosition);
            IMovementController movementController = new MovementController(transform);

            ILeg leftLeg = CreateLeg(Sides.Left, leftLegPos, transform);
            ILeg rightLeg = CreateLeg(Sides.Right, rightLegPos, transform);

            movementController.AddLeg(leftLeg);
            movementController.AddLeg(rightLeg);

            titanCharacter.SetTransform(transform);
            titanCharacter.SetMovementController(movementController);

            titanCharacter.Move();

            Assert.That(leftLeg.transform.Position != leftLegPos);
            Assert.That(rightLeg.transform.Position != rightLegPos);
        }

        private void AddLegs(int count, IMovementController movementController)
        {
            for (int i=0; i<count; i++)
            {
                ILeg leg = Substitute.For<ILeg>();
                movementController.AddLeg(leg);
            }
        }

        private Leg CreateLeg(Sides side, Vector3 pos, ITransformProxy titanTransform)
        {
            LegSetupPack legSetupPack = new LegSetupPack();

            legSetupPack.side = side;
            legSetupPack.transform = new FakeTransformProxy(pos);
            legSetupPack.titanTransform = titanTransform;
            legSetupPack.speed = 3f;
            legSetupPack.spacing = 47f;
            legSetupPack.stepDistance = 10f;

            Leg leg = new Leg(legSetupPack);
            return leg;
        }
    }
}
