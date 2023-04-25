using System;
using UnityEngine;

namespace Character
{
    public class CharacterHealthSystem : BaseController, IDamageable, IHealthSystem
    {
        private PlayerCharacter main;
        private HealthData healthData;
        private HitBoxProxy hitBox;
        private EventManager eventManager;

        private EventTrigger onDeathTrigger;

        private event Action<int> onHealthUpdate;
        private event Action onDeath;

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
                onHealthUpdate?.Invoke(healthData.Amount);
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

        public void SubscribeOnUpdate(Action<int> onHealthUpdate) => this.onHealthUpdate += onHealthUpdate;
        public void SubscribeOnDeath(Action onDeath) => this.onDeath += onDeath;

        public override void Initialize(PlayerCharacter main)
        {
            this.main = main;
            healthData = main.Data.Health;
            hitBox = main.HitBox;
            eventManager = main.Events;
        }

        public void Initialize(HealthData data) => this.healthData = data;

        public override void Connect()
        {
            hitBox.Subscribe(this);

            onDeathTrigger = new EventTrigger();
            //SubscribeOnDeath(() => onDeathTrigger.SetActive(true));

            SubscribeOnUpdate(HealthBar.Instance.OnUpdate);
            onHealthUpdate?.Invoke(healthData.Amount);

            SubscribeOnDeath(() => main.SetActive(false));
            eventManager.ConnectTrigger(onDeathTrigger, "PlayerDied");

            GameEvents.current.onVictory += BecomeImmortal;
        }

        public override void Disconnect() => hitBox.Unsubscribe(this);

        public void Die()
        {
            healthData.Amount = 0;
            onDeath?.Invoke();
            GameEvents.current.PlayerHasDied();
        }

        private void BecomeImmortal(){
            healthData.IsImmortal = true;
        }
    }
}