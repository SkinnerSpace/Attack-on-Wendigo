using UnityEngine;

public class LegRaycaster : ILegRaycaster
{
    private const int GROUND_LAYER = 1 << 8;

    private readonly ILeg leg;
    private readonly ITransformProxy pivotPoint;

    private float stepDistance;
    private float stepSpacing;

    public LegRaycaster(ILeg leg, ITransformProxy pivotPoint)
    {
        this.leg = leg;
        this.pivotPoint = pivotPoint;
    }

    public void SetSpacingAndStepDistance(float stepSpacing, float stepDistance)
    {
        this.stepSpacing = stepSpacing;
        this.stepDistance = stepDistance;
    }

    public Vector3 GetNextStepPosition()
    {
        Vector3 nextPos = Vector3.zero;

        Ray ray = new Ray(GetNextStepPoint(), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
            nextPos = raycastHit.point;

        return nextPos;
    }

    public Vector3 GetStandPos()
    {
        Vector3 standPos = Vector3.zero;

        Ray ray = new Ray(GetStandPoint(), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
            standPos = raycastHit.point;

        return standPos;
    }

    public Vector3 GetNextStepPoint()
    {
        return (GetStandPoint() + (pivotPoint.Forward * stepDistance));
    }

    public Vector3 GetStandPoint()
    {
        return pivotPoint.Position + (pivotPoint.Right * (stepSpacing * leg.Side));
    }
}
