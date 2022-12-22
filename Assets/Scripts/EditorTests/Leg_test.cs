using UnityEngine;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class Leg_test
    {
        [Test]
        public void Leg_is_created()
        {
            ILeg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            Assert.That(leg != null);
        }

        [Test]
        public void Leg_has_raycaster()
        {
            Leg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            LegRaycaster Raycaster = new LegRaycaster(leg, Substitute.For<ITransformProxy>(), 1f, 1f);
            leg.SetRaycaster(Raycaster);

            Assert.That(leg.Raycaster != null);
        }

        [Test]
        public void Leg_has_engine()
        {
            Leg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            LegEngine Engine = new LegEngine(leg, 1f, 1f);
            leg.SetEngine(Engine);

            Assert.That(leg.Engine != null);
        }

        [Test]
        public void Leg_has_synchronizer()
        {
            MovementController movementController = new MovementController(Substitute.For<ITransformProxy>());
            Leg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            movementController.AddLeg(leg);

            Assert.That(leg.Synchronizer != null);
        }

        [Test]
        public void Leg_has_foot()
        {
            Leg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            Assert.That(leg.Foot != null);
        }

        [Test]
        public void Update_is_called()
        {
            MovementController movementController = new MovementController(Substitute.For<ITransformProxy>());
            ILeg leg = Substitute.For<ILeg>();
            movementController.AddLeg(leg);
            movementController.MoveLegs();

            leg.Received().Update();
        }

        [Test]
        public void Next_step_position_is_set()
        {
   
            ITransformProxy transformProxy = new FakeTransformProxy(Vector3.zero);
            Leg leg = new Leg(Sides.Left, transformProxy);

            ILegRaycaster Raycaster = Substitute.For<ILegRaycaster>();
            Raycaster.GetNextStepPosition().Returns(Vector3.one);
            leg.SetRaycaster(Raycaster);

            ILegEngine Engine = Substitute.For<ILegEngine>();
            leg.SetEngine(Engine);
            leg.SetNextStep();

            leg.Update();

            Assert.That(leg.StepPos.novel != Vector3.zero);
        }

        [Test]
        public void Position_is_updated()
        {
            ITransformProxy transformProxy = new FakeTransformProxy(Vector3.zero);
            Leg leg = new Leg(Sides.Left, transformProxy);

            leg.SetRaycaster(Substitute.For<ILegRaycaster>());

            ILegEngine Engine = Substitute.For<ILegEngine>();
            Engine.UpdatePosition(Vector3.zero, Vector3.zero).Returns(Vector3.one);
            leg.SetEngine(Engine);
            leg.SetNextStep();

            leg.Update();
            Assert.AreEqual(Vector3.one, leg.StepPos.current);
        }
    }
}
