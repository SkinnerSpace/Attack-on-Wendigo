﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class Character_health_system_tests
    {
        [Test]
        public void Health_system_exist()
        {
            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            Assert.That(healthSystem != null);
        }

        [Test]
        public void Character_is_alive_on_start()
        {
            ICharacterData data = Substitute.For<ICharacterData>();
            data.Health.Returns(10);

            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            healthSystem.Initialize(data);
            Assert.That(healthSystem.IsAlive);
        }

        [Test]
        public void Character_receives_damage()
        {
            int initialHealth = 10;
            int damageAmount = 1;

            ICharacterData data = new MockCharacterData() { Health = initialHealth };

            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            healthSystem.Initialize(data);

            DamagePackage damage = new DamagePackage(damageAmount);
            healthSystem.ReceiveDamage(damage);

            Assert.AreEqual(initialHealth - damageAmount, healthSystem.Health);
        }

        [Test]
        public void Health_never_falls_below_zero()
        {
            int initialHealth = 10;
            int damageAmount = 20;

            ICharacterData data = new MockCharacterData() { Health = initialHealth };

            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            healthSystem.Initialize(data);

            DamagePackage damage = new DamagePackage(damageAmount);
            healthSystem.ReceiveDamage(damage);

            Assert.AreEqual(0, healthSystem.Health);
        }

        [Test]
        public void If_is_dead_then_health_is_below_zero()
        {
            ICharacterData data = new MockCharacterData() { Health = 10 };

            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            healthSystem.Initialize(data);

            healthSystem.Die();

            Assert.AreEqual(0, healthSystem.Health);
        }

        [Test]
        public void If_is_dead_then_not_alive()
        {
            ICharacterData data = new MockCharacterData() { Health = 10 };

            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            healthSystem.Initialize(data);

            healthSystem.Die();

            Assert.False(healthSystem.IsAlive);
        }

        [Test]
        public void Health_observer_is_notified_on_update()
        {
            int initialHealth = 10;
            int damageAmount = 1;

            ICharacterData data = new MockCharacterData() { Health = initialHealth };

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

            ICharacterData data = new MockCharacterData() { Health = initialHealth };

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

            ICharacterData data = new MockCharacterData() { Health = initialHealth };

            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            healthSystem.Initialize(data);

            DamagePackage damage = new DamagePackage(damageAmount);
            healthSystem.ReceiveDamage(damage);

            IHealthObserver observer = Substitute.For<IHealthObserver>();
            healthSystem.SubscribeOnDeath(observer.OnDeath);

            healthSystem.ReceiveDamage(damage);

            observer.DidNotReceive().OnDeath();
        }

        [Test]
        public void Character_receives_impact()
        {
            int initialHealth = 10;
            int damageAmount = 1;
            Vector3 impactForce = Vector3.one;

            ICharacterData data = new MockCharacterData() { Health = initialHealth };

            CharacterHealthSystem healthSystem = new CharacterHealthSystem();
            healthSystem.Initialize(data);

            DamagePackage damage = new DamagePackage(damageAmount, impactForce, Vector3.zero);
            healthSystem.ReceiveDamage(damage);
            data.UpdateVelocity();

            Assert.AreEqual(impactForce, data.Velocity);
        }
    }
}