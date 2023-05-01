using System;

namespace Character
{
    public class CharacterBurnController : BaseController
    {
        private CharacterHealthSystem healthSystem;
        private BurnHandler burnHandler;

        private float scorchTime = 1f;
        private int scorchDamage = 1;

        public event Action onBurn;

        public override void Initialize(PlayerCharacter main)
        {
            healthSystem = main.GetController<CharacterHealthSystem>();
            burnHandler = new BurnHandler(main.FireHitBox, main.Timer, scorchTime);
        }

        public override void Connect() => burnHandler.onScorchDamage += ReceiveDamage;
        public override void Disconnect() { }

        public void Subscribe(IBurnObserver observer) => burnHandler.Subscribe(observer);

        private void ReceiveDamage(bool criticalDamage)
        {
            DamagePackage damagePackage = new DamagePackage(scorchDamage);
            damagePackage.damageType = DamageTypes.Fire;

            if (criticalDamage){
                healthSystem.ReceiveDamage(damagePackage);
            }
            else{
                healthSystem.ReceiveNonCriticalDamage(damagePackage);
            }

            onBurn?.Invoke();
        }
    }
}