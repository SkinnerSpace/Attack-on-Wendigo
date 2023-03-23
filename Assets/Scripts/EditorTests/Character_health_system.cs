using NUnit.Framework;
using NSubstitute;
using Character;

namespace Tests
{
    namespace Player_character
    {
        public class Character_health_system
        {
#pragma warning disable CS1701

            [Test]
            public void Is_alive_on_start()
            {
                HealthData health = new HealthData() { Amount = 10 };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(health);
                Assert.That(health.IsAlive);
            }

            [Test]
            public void Receives_damage()
            {
                int initialHealth = 10;
                int damageAmount = 1;
                int expectedHealth = initialHealth - damageAmount;

                HealthData health = new HealthData() { Amount = initialHealth };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(health);

                DamagePackage damage = new DamagePackage(damageAmount);
                healthSystem.ReceiveDamage(damage);

                Assert.AreEqual(expectedHealth, health.Amount);
            }

            [Test]
            public void Health_never_falls_below_zero()
            {
                int initialHealth = 10;
                int damageAmount = 20;

                HealthData health = new HealthData() { Amount = initialHealth };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(health);

                DamagePackage damage = new DamagePackage(damageAmount);
                healthSystem.ReceiveDamage(damage);

                Assert.AreEqual(0, health.Amount);
            }

            [Test]
            public void If_is_dead_then_health_is_zero()
            {
                HealthData data = new HealthData() { Amount = 10 };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(data);

                healthSystem.Die();

                Assert.AreEqual(0, data.Amount);
            }

            [Test]
            public void If_is_dead_then_not_alive()
            {
                HealthData data = new HealthData { Amount = 10 };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(data);
                healthSystem.Die();

                Assert.False(data.IsAlive);
            }

            [Test]
            public void Health_observer_is_notified_on_update()
            {
                int initialHealth = 10;
                int damageAmount = 1;

                HealthData data = new HealthData() { Amount = initialHealth };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(data);

                IHealthObserver observer = Substitute.For<IHealthObserver>();
                healthSystem.SubscribeOnUpdate(observer.OnHealthUpdate);

                DamagePackage damage = new DamagePackage(damageAmount);
                healthSystem.ReceiveDamage(damage);

                observer.Received().OnHealthUpdate(initialHealth - damageAmount);
            }

            [Test]
            public void Character_dies_when_health_is_lower_or_equal_to_zero()
            {
                int initialHealth = 1;
                int damageAmount = 1;

                HealthData data = new HealthData() { Amount = initialHealth };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(data);

                IHealthObserver observer = Substitute.For<IHealthObserver>();
                healthSystem.SubscribeOnDeath(observer.OnDeath);

                DamagePackage damage = new DamagePackage(damageAmount);
                healthSystem.ReceiveDamage(damage);

                observer.Received().OnDeath();
            }

            [Test]
            public void Can_not_die_more_than_once()
            {
                int initialHealth = 1;
                int damageAmount = 1;

                HealthData data = new HealthData() { Amount = initialHealth };

                CharacterHealthSystem healthSystem = new CharacterHealthSystem();
                healthSystem.Initialize(data);

                DamagePackage damage = new DamagePackage(damageAmount);
                healthSystem.ReceiveDamage(damage);

                IHealthObserver observer = Substitute.For<IHealthObserver>();
                healthSystem.SubscribeOnDeath(observer.OnDeath);

                healthSystem.ReceiveDamage(damage);

                observer.DidNotReceive().OnDeath();
            }
        }
    }
}