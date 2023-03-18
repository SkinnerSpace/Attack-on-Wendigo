using System;
using UnityEngine;

namespace Character
{
    public class CharacterHealthSystem : BaseController, IDamageable
    {
        private PlayerCharacter main;
        private HealthData healthData;
        private HitBoxProxy hitBox;
        private EventManager eventManager;

        private EventTrigger onDeathTrigger;

        private event Action<int> onHealthUpdate;
        private event Action onDeath;

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            if (healthData.IsAlive)
            {
                healthData.Amount -= damagePackage.damage;

                if (!healthData.IsAlive){
                    Die();
                }

                onHealthUpdate?.Invoke(healthData.Amount);
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

            onDeathTrigger = new EventTrigger();
            SubscribeOnDeath(() => onDeathTrigger.SetActive(true));

            SubscribeOnUpdate(HealthBar.Instance.OnUpdate);
        }

        public void Initialize(HealthData data) => this.healthData = data;

        public override void Connect()
        {
            hitBox.Subscribe(this);
            SubscribeOnDeath(() => main.SetActive(false));
            eventManager.ConnectTrigger(onDeathTrigger, "PlayerDied");
        }

        public override void Disconnect() => hitBox.Unsubscribe(this);

        public void Die()
        {
            healthData.Amount = 0;
            onDeath?.Invoke();
        }
    }

    public class ImpactReceiver : BaseController, IDamageable
    {
        private ICharacterData oldData;
        private HitBoxProxy hitBox;

        private event Action<float> onImpact;

        public override void Initialize(PlayerCharacter main)
        {
            oldData = main.OldData;
            hitBox = main.HitBox;
        }

        public void Initialize(ICharacterData oldData) => this.oldData = oldData;

        public override void Connect()
        {
            hitBox.Subscribe(this);
        }

        public override void Disconnect()
        {
            hitBox.Unsubscribe(this);
        }

        public void SubscribeOnImpact(Action<float> onImpact) => this.onImpact += onImpact;

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            oldData.AddVelocity(damagePackage.impact);
            onImpact?.Invoke(damagePackage.impact.magnitude);
        }
    }
}