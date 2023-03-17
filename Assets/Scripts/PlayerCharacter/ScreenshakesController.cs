using System;
using UnityEngine;

public class ScreenshakesController : BaseController
{
    private PlayerCharacter main;
    private ICharacterData data;
    private float maxImpact = 500f;

    public override void Initialize(PlayerCharacter main)
    {
        this.main = main;
        data = main.Data;
    }

    public override void Connect() => main.GetController<CharacterHealthSystem>().SubscribeOnImpact(ShakeOnImpactReceived);

    public override void Disconnect() { }

    private void ShakeOnImpactReceived(float impact)
    {
        float shakePower = (impact / maxImpact).Clamp01();
        data.GetShake("Impact").SetPower(shakePower).Launch(ShakeManagerComponent.Instance);
    }
}
