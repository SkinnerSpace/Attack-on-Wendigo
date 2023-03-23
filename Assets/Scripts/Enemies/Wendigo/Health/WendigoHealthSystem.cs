using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoHealthSystem : WendigoPlugableComponent, IDamageable
    {
        public const float DEATH_IMPACT_MULTIPLIER = 5f;

        private HealthData data;

        private event Action onDeath;
        private event Action<Vector3, Vector3> onImpactApply;

        public override void Initialize(Wendigo wendigo)
        {
            SetData(wendigo.Data.Health);

            foreach (IHitBox hitBox in wendigo.HitBoxes)
                ConnectToHitBox(hitBox);
        }

        public void SetData(HealthData data) => this.data = data;
        public void ConnectToHitBox(IHitBox hitBox) => hitBox.Subscribe(this);

        public void SubscribeOnDeath(Action onDeath) => this.onDeath += onDeath;
        public void SubscribeOnImpactApply(Action<Vector3, Vector3> onImpactApply) => this.onImpactApply += onImpactApply;

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            data.Amount -= damagePackage.damage;



            if (MustDie()){
                Die();
                ApplyDeathImpact(damagePackage);
            }
            else if (IsDead()){
                ApplyImpact(damagePackage);
            }
        }

        private void ApplyDeathImpact(DamagePackage damagePackage){
            onImpactApply?.Invoke(damagePackage.impact * DEATH_IMPACT_MULTIPLIER, damagePackage.point);
        }

        private void ApplyImpact(DamagePackage damagePackage){
            onImpactApply?.Invoke(damagePackage.impact, damagePackage.point);
        }

        private bool MustDie() => data.Amount <= 0 && data.IsAlive;

        private bool IsDead() => !data.IsAlive;

        private void Die()
        {
            data.Amount = 0;
            data.IsAlive = false;
            onDeath?.Invoke();
        }
    }
}
