using UnityEngine;

public class WendigoRotationController : WendigoBaseController
{
    private IWendigo wendigo;
    private WendigoData data;
    private IChronos chronos;

    public override void Initialize(IWendigo wendigo)
    {
        this.wendigo = wendigo;
        data = wendigo.Data;
        chronos = wendigo.Chronos;
    } 

    public void RotateToTarget(Vector3 targetPos)
    {
        Vector3 flatTargetPos = new Vector3(targetPos.x, 0f, targetPos.z);
        Vector3 flatOwnPos = new Vector3(data.Position.x, 0f, data.Position.z);

        Vector3 dirToTarget = (flatTargetPos - flatOwnPos).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dirToTarget, Vector3.up);

        data.Rotation = Quaternion.RotateTowards(data.Rotation, targetRotation, data.RotationSpeed * chronos.DeltaTime);
    }
}
