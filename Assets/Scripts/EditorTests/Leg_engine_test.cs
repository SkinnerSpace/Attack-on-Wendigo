﻿using NUnit.Framework;
using UnityEngine;
using NSubstitute;

namespace Tests
{
    public class Leg_engine_test
    {
        private readonly float speed = 3f;
        private readonly float stepHeight = 10f;
        private readonly float delta = 0.1f;

        [Test]
        public void Leg_engine_is_created()
        {
            ILeg leg = Substitute.For<ILeg>();
            ILegEngine engine = new LegEngine(leg, speed, stepHeight);

            Assert.That(engine != null);
        }

        [Test]
        public void Leg_engine_has_unity_service()
        {
            ILeg leg = Substitute.For<ILeg>();
            LegEngine engine = new LegEngine(leg, speed, stepHeight);

            Assert.That(engine.UnityService != null);
        }

        [Test]
        public void Lerp_is_incremented_correctly()
        {
            int cycles = 3;

            ILeg leg = Substitute.For<ILeg>();
            LegEngine engine = new LegEngine(leg, speed, stepHeight);

            IUnityService UnityService = Substitute.For<IUnityService>();
            UnityService.Delta.Returns(delta);
            engine.SetUnityService(UnityService);

            float expectedLerp = (speed * delta) * cycles;

            for (int i=0; i<cycles; i++)
                engine.IncrementLerp(speed);

            Assert.AreEqual(expectedLerp, engine.Lerp);
        }

        [Test]
        public void Position_is_calculated_correctly()
        {
            Vector3 oldPos = Vector3.zero;
            Vector3 newPos = Vector3.one;
            float lerp = 0.5f;

            ILeg leg = Substitute.For<ILeg>();
            LegEngine engine = new LegEngine(leg, speed, stepHeight)
            {
                Lerp = lerp
            };

            Vector3 expectedPosition = Vector3.Lerp(oldPos, newPos, lerp);
            expectedPosition.y += (Mathf.Sin(lerp * Mathf.PI) * stepHeight);

            Vector3 calculatedPosition = engine.CalculatePosition(oldPos, newPos);

            Assert.AreEqual(expectedPosition, calculatedPosition);
        }

        [Test]
        public void Step_is_over()
        {
            float speed = 10f;
            float delta = 1f;

            ILeg leg = Substitute.For<ILeg>();

            LegEngine engine = new LegEngine(leg, speed, StepHeight: 1f);
            IUnityService UnityService = Substitute.For<IUnityService>();
            UnityService.Delta.Returns(delta);
            engine.SetUnityService(UnityService);

            engine.IncrementLerp(speed);
            engine.ResetLerpIfMax();

            leg.Received().StepIsOver();
            Assert.AreEqual(0f, engine.Lerp);
        }
    }
}