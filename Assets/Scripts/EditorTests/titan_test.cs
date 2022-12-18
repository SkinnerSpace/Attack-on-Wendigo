using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class titan_test
    {
        [Test]
        public void gingerbread_is_created()
        {
            TitanData data = new TitanData();
            Titan gingerbread = TitansFactory.CreateTitan(data);

            Assert.That(gingerbread.GetType() == typeof(Gingerbread));
        }

        [Test]
        public void gingerbread_is_standing()
        {
            Vector3 originalPosition = new Vector3(10f, 0f, 10f);

            TitanData data = new TitanData();
            data.transform = new TransformProxy(originalPosition);

            Gingerbread gingerbread = new Gingerbread(data);

            Assert.AreEqual(originalPosition, gingerbread.data.transform.position);
        }

        [Test]
        public void gingerbread_is_moving()
        {
            Vector3 originalPosition = new Vector3(10f, 0f, 10f);

            TitanData data = new TitanData();
            data.transform = new TransformProxy(originalPosition);

            Gingerbread gingerbread = new Gingerbread(data);
            MovementController movementController = CreateMovementController(0, data.transform);
            gingerbread.SetMovementController(movementController);
            gingerbread.Move();

            Assert.That(gingerbread.data.transform.position != originalPosition);
        }

        [Test]
        public void gingerbread_has_movement_controller()
        {
            TitanData data = new TitanData();
            Titan gingerbread = TitansFactory.CreateTitan(data);

            MovementController movementController = CreateMovementController(0, data.transform);
            gingerbread.SetMovementController(movementController);

            Assert.That(gingerbread.movementController != null);
        }

        [Test]
        public void gingerbread_has_at_least_one_leg()
        {
            TitanData data = new TitanData();
            Titan gingerbread = TitansFactory.CreateTitan(data);

            MovementController movementController = CreateMovementController(1, data.transform);
            gingerbread.SetMovementController(movementController);

            Assert.True(gingerbread.movementController.legsSync.legs.Count > 0);
        }

        [Test]
        public void gingerbread_has_two_legs()
        {
            TitanData data = new TitanData();
            Titan gingerbread = TitansFactory.CreateTitan(data);

            MovementController movementController = CreateMovementController(2, data.transform);
            gingerbread.SetMovementController(movementController);

            Assert.True(gingerbread.movementController.legsSync.legs.Count == 2);
        }

        [Test]
        public void gingerbread_cant_have_more_than_two_legs()
        {
            TitanData data = new TitanData();
            Titan gingerbread = TitansFactory.CreateTitan(data);

            MovementController movementController = CreateMovementController(3, data.transform);
            gingerbread.SetMovementController(movementController);

            Assert.True(gingerbread.movementController.legsSync.legs.Count < 2);
        }

        [Test]
        public void gingerbread_moves_correctly()
        {
            Assert.That(false);
        }

        private MovementController CreateMovementController(int legsCount, TransformProxy transform)
        {
            List<ILeg> legs = new List<ILeg>();

            for (int i=0; i<legsCount; i++)
            {
                LegSetupPack legSetupPack = new LegSetupPack();
                Leg leg = new Leg(legSetupPack);
                legs.Add(leg);
            }

            LegsSync legsSync = new LegsSync(legs);
            MovementController movementController = new MovementController(legsSync, transform);

            return movementController;
        }
    }
}
