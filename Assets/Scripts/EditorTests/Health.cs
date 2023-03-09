using WendigoCharacter;
using NUnit.Framework;

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
        }
    }
}
