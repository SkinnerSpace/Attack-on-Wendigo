using WendigoCharacter;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    namespace Wendigo
    {
        public class Health
        {
#pragma warning disable CS1701
            [Test]
            public void Receives_damage()
            {
                int initialHealth = 10;
                int damage = 1;

                WendigoHealthSystem healthSystem = new WendigoHealthSystem();
                HealthData health = new HealthData() { Amount = initialHealth };
                healthSystem.SetData(health);

                DamagePackage damagePackage = new DamagePackage(damage);
                healthSystem.ReceiveDamage(damagePackage);

                int expectedHealth = initialHealth - damage;
                Assert.AreEqual(expectedHealth, health.Amount);
            }

            [Test]
            public void Receives_damage_from_a_hit_box()
            {
                int initialHealth = 10;
                int damage = 1;

                WendigoHealthSystem healthSystem = new WendigoHealthSystem();
                HealthData health = new HealthData() { Amount = initialHealth };
                healthSystem.SetData(health);

                HitBox hitBox = new HitBox();
                healthSystem.ConnectToHitBox(hitBox);

                DamagePackage damagePackage = new DamagePackage(damage);
                hitBox.ReceiveDamage(damagePackage);

                int expectedHealth = initialHealth - damage;
                Assert.AreEqual(expectedHealth, health.Amount);
            }

            [Test]
            public void Impact_is_applied()
            {
                Vector3 impact = Vector3.up;
                Vector3 point = Vector3.one;

                Vector3 impactReceived = Vector3.zero;
                Vector3 pointReceived = Vector3.zero;

                void ReceiveImpact(Vector3 inImpact, Vector3 inPoint){
                    impactReceived = inImpact;
                    pointReceived = inPoint;
                }

                HealthData health = new HealthData();
                WendigoHealthSystem healthSystem = new WendigoHealthSystem();
                healthSystem.SetData(health);

                healthSystem.SubscribeOnImpactApply(ReceiveImpact);

                DamagePackage damagePackage = new DamagePackage(0, impact, point);
                healthSystem.ReceiveDamage(damagePackage);

                Assert.AreEqual(impact * WendigoHealthSystem.DEATH_IMPACT_MULTIPLIER, impactReceived);
                Assert.AreEqual(point, pointReceived);
            }

            [Test]
            public void Dies_when_health_less_than_or_equal_to_zero()
            {
                int initialHealth = 1;
                int damage = 1;

                WendigoHealthSystem healthSystem = new WendigoHealthSystem();
                HealthData health = new HealthData() { Amount = initialHealth };
                healthSystem.SetData(health);

                DamagePackage damagePackage = new DamagePackage(damage);
                healthSystem.ReceiveDamage(damagePackage);

                Assert.That(!health.IsAlive);
            }

            [Test]
            public void Can_not_die_more_than_once()
            {
                int initialHealth = 1;
                int damage = 1;
                int deathCount = 0;

                void IncrementDeathCount() { deathCount++; }

                WendigoHealthSystem healthSystem = new WendigoHealthSystem();
                HealthData health = new HealthData() { Amount = initialHealth };
                healthSystem.SetData(health);

                healthSystem.SubscribeOnDeath(IncrementDeathCount);

                DamagePackage damagePackage = new DamagePackage(damage);
                healthSystem.ReceiveDamage(damagePackage);
                healthSystem.ReceiveDamage(damagePackage);

                Assert.AreEqual(1, deathCount);
            }
        }
    }
}
