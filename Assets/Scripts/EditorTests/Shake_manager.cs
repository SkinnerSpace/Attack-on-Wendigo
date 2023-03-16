using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    namespace ScreenShakes
    {
        public class Shake_manager
        {
            [Test]
            public void Shake_is_added_to_the_manager()
            {
                IShakeable shakeable = Substitute.For<IShakeable>();
                ShakeManager shakeManager = new ShakeManager(shakeable);

                Shake shake = new Shake();
                shakeManager.AddAndLaunch(shake);

                Assert.AreEqual(1, shakeManager.GetShakesCount());
            }

            [Test]
            public void Displacement_is_set()
            {
                ShakeDisplacement displacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                Assert.AreEqual(Vector3.one, displacement.position);
                Assert.AreEqual(Vector3.one, displacement.angle);
            }

            [Test]
            public void Displacement_is_accumulated()
            {
                ShakeDisplacement firstDisplacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                ShakeDisplacement secondDisplacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                firstDisplacement.Accumulate(secondDisplacement);

                Assert.AreEqual(Vector3.one * 2f, firstDisplacement.position);
                Assert.AreEqual(Vector3.one * 2f, firstDisplacement.angle);
            }

            [Test]
            public void Displacement_is_cleared()
            {
                ShakeDisplacement displacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                displacement.Clear();
                Assert.AreEqual(Vector3.zero, displacement.position);
                Assert.AreEqual(Vector3.zero, displacement.angle);
            }
        }
    }
}
