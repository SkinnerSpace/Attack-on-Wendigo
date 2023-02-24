using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Wendigo_tests
    {
        [Test]
        public void Receive_damage()
        {
            WendigoData data = MockWendigoData.Create();
            data.Health = 3;

            IWendigo wendigo = new MockWendigo(data).WithHitBoxes(3);
            WendigoHealthSystem healthSystem = new WendigoHealthSystem(wendigo);

            wendigo.HitBoxes[0].ReceiveDamage(new DamagePackage(1));

            Assert.AreEqual(2, data.Health);
        }
    }
}
