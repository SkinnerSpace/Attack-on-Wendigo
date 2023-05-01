using System;
using UnityEngine;

namespace Character
{
    public class ScreenshakesController : BaseController
    {
        private PlayerCharacter main;
        private ICharacterData data;
        private float maxImpact = 400f;

        public override void Initialize(PlayerCharacter main)
        {
            this.main = main;
            data = main.OldData;
        }

        public override void Connect()
        {
            main.GetController<ImpactReceiver>().SubscribeOnImpact(ShakeOnImpactReceived);
            main.GetController<CharacterBurnController>().onBurn += ShakeOnBurn;
        }

        public override void Disconnect() { }

        public void StopShake()
        {
            ShakeManagerComponent.Instance.Stop();
        }

        private void ShakeOnImpactReceived(float impact)
        {
            float shakePower = (impact / maxImpact);
            shakePower = Mathf.Clamp(shakePower, 0.3f, 1f);

            data.GetShake("Impact").SetPower(shakePower).Launch(ShakeManagerComponent.Instance);
        }

        private void ShakeOnBurn()
        {
            float power = Rand.Range(0.7f, 1f);
            data.GetShake("Burn").SetPower(power).Launch(ShakeManagerComponent.Instance);
        }
    }
}