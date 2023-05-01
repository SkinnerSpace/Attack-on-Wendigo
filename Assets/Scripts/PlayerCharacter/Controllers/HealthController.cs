using UnityEngine;

namespace Character
{
    public class HealthController
    {
        private HealthData healthData;

        public HealthController(HealthData healthData) => this.healthData = healthData;

        public bool IsAlive() => healthData.amount > 0;
        public void BecomeImmortal()
        {
            healthData.isImmortal = true;
        }

        public bool IsDamageable()
        {
            return healthData.amount > 0 &&
                   !healthData.isImmortal;
        }

        public bool CheckIfCritical(int decrement) => (healthData.amount - decrement) <= 0;

        public void Set(int value)
        {
            healthData.amount = value;
            PlayerEvents.current.NotifyOnHealthUpdate(GetPercent());
        }

        public void Reduce(int decrement)
        {
            healthData.amount -= decrement;
            ClampHealthAmount();
            PlayerEvents.current.NotifyOnHealthUpdate(GetPercent());
        }

        public void Increase(int increment)
        {
            healthData.amount += increment;
            ClampHealthAmount();
            PlayerEvents.current.NotifyOnHealthUpdate(GetPercent());
        }

        private void ClampHealthAmount() => healthData.amount = Mathf.Clamp(healthData.amount, 0, healthData.maxAmount);

        public int GetPercent() => (int)((healthData.amount / (float)healthData.maxAmount) * 100f);
    }
}