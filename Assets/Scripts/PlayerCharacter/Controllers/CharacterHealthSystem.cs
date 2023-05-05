using System;
using UnityEngine;

namespace Character
{
    public class CharacterHealthSystem : BaseController, IDamageable, IHealthSystem
    {
        private HealthController health;
        private HitBoxProxy hitBox;

        public void Initialize(HealthData healthData) => health = new HealthController(healthData);

        public override void Connect()
        {
            hitBox.Subscribe(this);

            GameEvents.current.onVictory += () => health.BecomeImmortal();
        }

        public override void Disconnect() => hitBox.Unsubscribe(this);

        public void ReceiveNonCriticalDamage(DamagePackage damagePackage)
        {
            if (!health.CheckIfCritical(damagePackage.damage)){
                ReceiveDamage(damagePackage);
            }
            else if (health.IsAlive())
            {
                health.Set(1);
            }
        }

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            if (health.IsDamageable()){
                health.Reduce(damagePackage.damage);

                if (!health.IsAlive()){
                    Die(damagePackage.damageType);
                }

                PlayerEvents.current.NotifyOnDamage();
                NotifyOnBluntDamage(damagePackage);
            }
        }

        private void NotifyOnBluntDamage(DamagePackage damagePackage)
        {
            if (damagePackage.damageType == DamageTypes.Blunt){
                PlayerEvents.current.NotifyOnReceivedBluntDamage();
            }
        }

        public override void Initialize(PlayerCharacter main)
        {
            health = new HealthController(main.Data.Health);
            hitBox = main.HitBox;
        }

        public void Die(DamageTypes damageType)
        {
            health.Set(0);
            PlayerEvents.current.NotifyOnDeath();

            if (damageType == DamageTypes.Blunt){
                PlayerEvents.current.NotifyOnHammered();
            }
            else if (damageType == DamageTypes.Fire){
                PlayerEvents.current.NotifyOnBurnt();
            }
        }

        public void RestoreHealth(int healthAmount)
        {
            health.Increase(healthAmount);
            PlayerEvents.current.NotifyOnHealthRestore();
        }

        public void SetHealth(int healthValue) => health.Set(healthValue);

        public void SubscribeOnDeath(Action onDeath) { }
    }
}