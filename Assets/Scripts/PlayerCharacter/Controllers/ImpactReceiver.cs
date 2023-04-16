using System;
using UnityEngine;

namespace Character
{
    public class ImpactReceiver : BaseController, IDamageable
    {
        private const float MAX_VERTICAL_IMPACT = 10f;

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

        // Screenshakes controller is subscribed on this event
        public void SubscribeOnImpact(Action<float> onImpact) => this.onImpact += onImpact;

        public void ReceiveDamage(DamagePackage damagePackage)
        {
            float verticalImpact = Mathf.Clamp(damagePackage.impact.y, -MAX_VERTICAL_IMPACT, MAX_VERTICAL_IMPACT);
            Vector3 impact = new Vector3(damagePackage.impact.x, verticalImpact, damagePackage.impact.z);

            oldData.AddVelocity(impact);
            onImpact?.Invoke(impact.magnitude);
        }
    }
}