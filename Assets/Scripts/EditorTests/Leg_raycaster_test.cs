using NUnit.Framework;
using UnityEngine;
using NSubstitute;

namespace Tests
{
    public class Leg_raycaster_test
    {
        private readonly float spacing = 47f;
        private readonly float stepDistance = 10f;

        [Test]
        public void Leg_raycaster_is_created()
        {
            ILeg leg = Substitute.For<ILeg>();
            ITransformProxy pivotPoint = new FakeTransformProxy(Vector3.zero);

            LegRaycaster raycaster = new LegRaycaster(leg, pivotPoint, spacing, stepDistance);
            Assert.That(raycaster != null);
        }

        [Test]
        public void Stand_point_is_correct()
        {
            ILeg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            ITransformProxy pivotPoint = new FakeTransformProxy(Vector3.zero);
            Vector3 expectedPoint = pivotPoint.Position + (pivotPoint.Right * (spacing * leg.Side));

            LegRaycaster raycaster = new LegRaycaster(leg, pivotPoint, spacing, stepDistance);
            Vector3 currentPoint = raycaster.GetStandPoint();

            Assert.AreEqual(expectedPoint, currentPoint);
        }

        [Test]
        public void Next_step_point_is_correct()
        {
            ILeg leg = new Leg(Sides.Left, Substitute.For<ITransformProxy>());
            ITransformProxy pivotPoint = new FakeTransformProxy(Vector3.zero);

            LegRaycaster raycaster = new LegRaycaster(leg, pivotPoint, spacing, stepDistance);

            Vector3 expectedPoint = raycaster.GetStandPoint() + (pivotPoint.Forward * stepDistance);
            Vector3 currentPoint = raycaster.GetNextStepPoint();

            Assert.AreEqual(expectedPoint, currentPoint);
        }
    }
}
