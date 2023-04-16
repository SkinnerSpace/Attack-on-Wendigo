using System;
using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoHealthSystem : WendigoPlugableComponent, IDamageable, IHealthSystem
    {
        public const float DEATH_IMPACT_MULTIPLIER = 5f;

        private HealthData data;

        private int initialLimbsCount;
        private int limbsCount;
        private float injuryDegree;

        private event Action onDeath;
        private event Action<Vector3, Vector3> onImpactApply;
        private event Action onInitialized;

        public event Action<float> onInjuryDegreeUpdate;

        public override void Initialize(Wendigo wendigo)
        {
            SetData(wendigo.Data.Health);
            ConnectToLimbs(wendigo.limbs);
            //ConnectToHitBoxes(wendigo.HitBoxes);
            onInitialized?.Invoke();
        }

        public void SetData(HealthData data) => this.data = data;
        private void ConnectToHitBoxes(IHitBox[] hitBoxes)
        {
            foreach (IHitBox hitBox in hitBoxes)
                ConnectToHitBox(hitBox);
        }
        public void ConnectToHitBox(IHitBox hitBox) => hitBox.Subscribe(this);

        public void ConnectToLimbs(Limb[] limbs){
            initialLimbsCount = limbs.Length;

            foreach (Limb limb in limbs){
                limb.onDamage += ReceiveDamage;
                limb.onInjury += OnInjury;
            }
        }

        public void SubscribeOnDeath(Action onDeath) => this.onDeath += onDeath;
        public void SubscribeOnImpactApply(Action<Vector3, Vector3> onImpactApply) => this.onImpactApply += onImpactApply;

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            data.Amount -= damagePackage.damage;
            data.Amount = Mathf.Clamp(data.Amount, 0, data.InitialAmount);

            if (MustDie()){
                Die();
                ApplyDeathImpact(damagePackage);
            }
            else if (IsDead()){
                ApplyImpact(damagePackage);
            }

            GameEvents.current.EnemyHealthHasBeenUpdated(data.Percent);
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

        private void OnInjury()
        {
            if (limbsCount > 0) {
                limbsCount -= 1;
            }

            injuryDegree = 1f - (limbsCount / (float)initialLimbsCount);
            onInjuryDegreeUpdate?.Invoke(injuryDegree);
        }

        public void OnSpawn()
        {
            limbsCount = initialLimbsCount;
            injuryDegree = 0f;
        }
    }
}
