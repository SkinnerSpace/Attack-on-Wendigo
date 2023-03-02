using UnityEngine;

public class WendigoRangeCombatManager : WendigoBaseController
{
    private WendigoData data;

    public override void Initialize(IWendigo wendigo)
    {
        data = wendigo.Data;
    }

    public void CheckReadinessToShoot()
    {
        if (data.Target != null && data.FireballIsReady)
        {
            float distanceToTarget = (data.Target.position.FlatV3() - data.Position.FlatV3()).magnitude;
            data.IsReadyToShoot = distanceToTarget >= data.FireballMinDistance && distanceToTarget <= data.FireballMaxDistance;
        }
        else
        {
            data.IsReadyToShoot = false;
        }
    }
}
