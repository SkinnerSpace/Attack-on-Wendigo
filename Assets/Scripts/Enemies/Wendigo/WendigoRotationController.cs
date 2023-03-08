using UnityEngine;

namespace WendigoCharacter
{
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
            Vector3 flatOwnPos = data.Transform.Position.FlatV3();

            Vector3 dirToTarget = (flatTargetPos - flatOwnPos).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(dirToTarget, Vector3.up);

            float percent = data.Movement.RotationSpeed * chronos.DeltaTime;
            data.Transform.Rotation = Quaternion.RotateTowards(data.Transform.Rotation, targetRotation, percent);
        }
    }
}
