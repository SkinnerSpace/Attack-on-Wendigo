using System;

namespace Character
{
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