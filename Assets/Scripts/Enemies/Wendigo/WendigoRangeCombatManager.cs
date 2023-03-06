using UnityEngine;

public class WendigoRangeCombatManager : WendigoBaseController
{
    private WendigoData data;

    public override void Initialize(IWendigo wendigo)
    {
        data = wendigo.Data;
    }

    public void PrepareToAttack()
    {
        float distance = TargetExist() ? GetDistance() : 0f;
        PrepareToBreathFire(distance);
        PrepareToCastAFireball(distance);
    }

    private bool TargetExist() => (data.Target != null);
    private float GetDistance() => (data.Target.position.FlatV3() - data.Position.FlatV3()).magnitude;

    private void PrepareToBreathFire(float distance) => data.IsReadyToBreathFire = IsAbleToUseAFirebreath() ? OnTheFirebreathDistance(distance) : false;
    private bool IsAbleToUseAFirebreath() => data.Target != null && data.TargetFitsFirebreathAngle && data.FirebreathAbilityIsCharged;
    private bool OnTheFirebreathDistance(float distance) => data.IsReadyToBreathFire = distance >= data.FirebreathMinDistance && distance <= data.FirebreathMaxDistance;

    private void PrepareToCastAFireball(float distance) => data.IsReadyToCast = IsAbleToUseAFireball() ? OnTheFireballDistance(distance) : false;
    private bool IsAbleToUseAFireball() => data.Target != null && data.TargetFitsFireballAngle && data.FireballAbilityIsCharged;
    private bool OnTheFireballDistance(float distance) => data.IsReadyToCast = distance >= data.FireballMinDistance && distance <= data.FireballMaxDistance;
}
