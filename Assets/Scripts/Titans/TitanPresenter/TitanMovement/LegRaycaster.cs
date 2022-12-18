using UnityEngine;

public class LegRaycaster
{
    private Leg leg;
    private const int GROUND_LAYER = 1 << 8;
    private const float STEP_DISTANCE = 7f;
    private const float SPACING = 4.8f;

    public LegRaycaster(Leg leg)
    {
        this.leg = leg;
    }

    public Vector3 GetNextStepPos(ITransformProxy transform)
    {
        Vector3 nextPos = Vector3.zero;

        Ray ray = new Ray(GetNextStepPoint(transform), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
            nextPos = raycastHit.point;

        return nextPos;
    }

    public Vector3 GetStandPos(ITransformProxy transform)
    {
        Vector3 standPos = Vector3.zero;

        Ray ray = new Ray(GetStandPoint(transform), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
            standPos = raycastHit.point;

        return standPos;
    }

    private Vector3 GetNextStepPoint(ITransformProxy transform)
    {
        return (GetStandPoint(transform) + (transform.Forward * STEP_DISTANCE));
    }

    private Vector3 GetStandPoint(ITransformProxy transform)
    {
        return transform.Position + (transform.Right * (SPACING * leg.Side));
    }
}
