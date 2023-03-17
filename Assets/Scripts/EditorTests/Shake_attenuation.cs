using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    namespace Shake_tests
    {
        public class Shake_attenuation
        {
            [Test]
            public void Attenuation_with_0_percent_distance()
            {
                ShakeAttenuation shakeAttenuation = new ShakeAttenuation(new Vector3(0f, 0f, 10f), 10f);
                float attenuation = shakeAttenuation.CalculateAttenuation(new Vector3(0f, 0f, 10f));

                Assert.AreEqual(1f, attenuation);
            }

            [Test]
            public void Attenuation_with_25_percent_distance()
            {
                ShakeAttenuation shakeAttenuation = new ShakeAttenuation(new Vector3(0f, 0f, 10f), 10f);
                float attenuation = shakeAttenuation.CalculateAttenuation(new Vector3(0f, 0f, 7.5f));

                Assert.AreEqual(0.5625f, attenuation);
            }

            [Test]
            public void Attenuation_with_50_percent_distance()
            {
                ShakeAttenuation shakeAttenuation = new ShakeAttenuation(new Vector3(0f, 0f, 10f), 10f);
                float attenuation = shakeAttenuation.CalculateAttenuation(new Vector3(0f, 0f, 5f));

                Assert.AreEqual(0.25f, attenuation);
            }

            [Test]
            public void Attenuation_with_75_percent_distance()
            {
                ShakeAttenuation shakeAttenuation = new ShakeAttenuation(new Vector3(0f, 0f, 10f), 10f);
                float attenuation = shakeAttenuation.CalculateAttenuation(new Vector3(0f, 0f, 2.5f));

                Assert.AreEqual(0.0625f, attenuation);
            }

            [Test]
            public void Attenuation_with_100_percent_distance()
            {
                ShakeAttenuation shakeAttenuation = new ShakeAttenuation(new Vector3(0f, 0f, 10f), 10f);
                float attenuation = shakeAttenuation.CalculateAttenuation(new Vector3(0f, 0f, 0f));

                Assert.AreEqual(0f, attenuation);
            }

            [Test]
            public void Attenuation_with_200_percent_distance()
            {
                ShakeAttenuation shakeAttenuation = new ShakeAttenuation(new Vector3(0f, 0f, 10f), 10f);
                float attenuation = shakeAttenuation.CalculateAttenuation(new Vector3(0f, 0f, -10f));

                Assert.AreEqual(0f, attenuation);
            }
        }
    }
}
