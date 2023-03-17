using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    namespace Shake_tests
    {
        public class Shake_manager
        {
            [Test]
            public void Shake_is_added_to_the_manager()
            {
                IShakeable shakeable = Substitute.For<IShakeable>();
                ShakeManager shakeManager = new ShakeManager(shakeable);

                ShakeBuilderImp.Create().BuildAndLaunch(shakeManager);

                Assert.AreEqual(1, shakeManager.GetShakesCount());
            }

            [Test]
            public void Displacement_is_set()
            {
                ShakeDisplacement displacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                Assert.AreEqual(Vector3.one, displacement.Position);
                Assert.AreEqual(Vector3.one, displacement.Angle);
            }

            [Test]
            public void Displacement_is_accumulated()
            {
                ShakeDisplacement firstDisplacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                ShakeDisplacement secondDisplacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                firstDisplacement.Accumulate(secondDisplacement);

                Assert.AreEqual(Vector3.one * 2f, firstDisplacement.Position);
                Assert.AreEqual(Vector3.one * 2f, firstDisplacement.Angle);
            }

            [Test]
            public void Displacement_is_cleared()
            {
                ShakeDisplacement displacement = new ShakeDisplacement(Vector3.one, Vector3.one);
                displacement.Clear();
                Assert.AreEqual(Vector3.zero, displacement.Position);
                Assert.AreEqual(Vector3.zero, displacement.Angle);
            }
        }
    }
}
