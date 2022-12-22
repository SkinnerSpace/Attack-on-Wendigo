using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Torso_test
    {
        [Test]
        public void Torso_moves_correctly()
        {
            Vector3 posDeviation = new Vector3(0f, 1f, 1f);
            Vector3 angleDeviation = new Vector3(0f, 1f, 1f);
            float torsoModifier = 10f;

            ITransformProxy transform = new FakeTransformProxy(Vector3.zero);
            Torso torso = new Torso(transform);

            ITorsoController torsoController = Substitute.For<ITorsoController>();
            torsoController.GetTorsoModifier().Returns(torsoModifier);
            torso.SetTorsoController(torsoController);
            torso.SetPosAndAngleDeviations(posDeviation, angleDeviation);

            Vector3 expectedLocalPosition = transform.LocalPosition + (posDeviation * Mathf.Abs(torsoModifier * Torso.INTENSITY_MODIFIER));
            Vector3 expectedLocalAngle = transform.LocalAngle + (angleDeviation * (torsoModifier * Torso.INTENSITY_MODIFIER));

            torso.Update();

            Assert.AreEqual(expectedLocalPosition, transform.LocalPosition);
            Assert.AreEqual(expectedLocalAngle, transform.LocalAngle);
        }
    }
}
