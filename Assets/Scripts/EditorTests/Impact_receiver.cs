using NUnit.Framework;
using UnityEngine;
using Character;

namespace Tests
{
    namespace Player_character
    {
        public class Impact_receiver
        {
            [Test]
            public void Receives_impact()
            {
                Vector3 impactForce = Vector3.one;

                ICharacterData data = new MockCharacterData();
                ImpactReceiver impactReceiver = new ImpactReceiver();
                impactReceiver.Initialize(data);

                DamagePackage damage = new DamagePackage(0, impactForce, Vector3.zero);
                impactReceiver.ReceiveDamage(damage);
                data.UpdateVelocity();

                Assert.AreEqual(impactForce, data.Velocity);
            }

            [Test]
            public void Receives_impact_via_hit_box()
            {
                Vector3 impactForce = Vector3.one;

                ICharacterData data = new MockCharacterData();
                ImpactReceiver impactReceiver = new ImpactReceiver();
                impactReceiver.Initialize(data);

                HitBox hitBox = new HitBox();
                hitBox.Subscribe(impactReceiver);

                DamagePackage damage = new DamagePackage(0, impactForce, Vector3.zero);
                hitBox.ReceiveDamage(damage);
                data.UpdateVelocity();

                Assert.AreEqual(impactForce, data.Velocity);
            }
        }
    }
}
