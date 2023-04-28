using System;
using UnityEngine;

namespace Character
{
    public class CharacterHealthSystem : BaseController, IDamageable, IHealthSystem
    {
        private PlayerCharacter main;
        private HealthData healthData;
        private HitBoxProxy hitBox;

        public void Initialize(HealthData healthData) => this.healthData = healthData;

        public override void Connect()
        {
            hitBox.Subscribe(this);

            PlayerEvents.current.UpdateHealth(healthData.Amount);
            GameEvents.current.onVictory += BecomeImmortal;
        }

        public override void Disconnect() => hitBox.Unsubscribe(this);

        public void ReceiveNonCriticalDamage(DamagePackage damagePackage)
        {
            if (healthData.Amount - damagePackage.damage > 0){
                ReceiveDamage(damagePackage);
            }
        }

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            if (IsDamageable()){
                ReduceHealth(damagePackage);
                NotifyOnBluntDamage(damagePackage);
                PlayerEvents.current.UpdateHealth(healthData.Amount);
            }
        }

        private bool IsDamageable(){
            return healthData.IsAlive && 
                   !healthData.IsImmortal;
        }

        private void ReduceHealth(DamagePackage damagePackage)
        {
            healthData.Amount -= damagePackage.damage;

            if (!healthData.IsAlive){
                Die();
            }
        }

        private void NotifyOnBluntDamage(DamagePackage damagePackage)
        {
            if (damagePackage.damageType == DamageTypes.Blunt){
                GameEvents.current.PlayerReceivedBluntDamage();
            }
        }

        public override void Initialize(PlayerCharacter main)
        {
            this.main = main;
            healthData = main.Data.Health;
            hitBox = main.HitBox;
        }

        public void Die()
        {
            healthData.Amount = 0;
            PlayerEvents.current.Die();
        }

        public void RestoreHealth(int healthAmount)
        {
            healthData.Amount += healthAmount;
            PlayerEvents.current.UpdateHealth(healthData.Amount);
        }

        private void BecomeImmortal(){
            healthData.IsImmortal = true;
        }

        public void SubscribeOnDeath(Action onDeath) { }
    }
}