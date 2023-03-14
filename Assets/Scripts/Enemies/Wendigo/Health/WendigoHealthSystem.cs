using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoHealthSystem : WendigoPlugableComponent, IDamageable
    {
        private HealthData data;

        private event Action onDeath;
        private event Action<Vector3, Vector3> onTriggerRagdoll;

        public override void Initialize(Wendigo wendigo)
        {
            SetData(wendigo.Data.Health);

            foreach (IHitBox hitBox in wendigo.HitBoxes)
                ConnectToHitBox(hitBox);
        }

        public void SetData(HealthData data) => this.data = data;
        public void ConnectToHitBox(IHitBox hitBox) => hitBox.Subscribe(this);


        public void Subscribe(Action onDeath) => this.onDeath += onDeath;

        public void SubscribeOnRagdoll(Action<Vector3, Vector3> onTriggerRagdoll) => this.onTriggerRagdoll += onTriggerRagdoll;

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            data.Amount -= damagePackage.damage;

            if (!data.IsAlive)
            {
                onTriggerRagdoll?.Invoke(damagePackage.impact, damagePackage.point);
                onDeath?.Invoke();
            }
        }
    }
}
