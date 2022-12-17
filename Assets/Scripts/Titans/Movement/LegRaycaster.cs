using UnityEngine;

public class LegRaycaster
{
    private Leg leg;
    private const int GROUND_LAYER = 1 << 8;
    private const float STEP_DISTANCE = 5f;
    private const float SPACING = 5f;

    public LegRaycaster(Leg leg)
    {
        this.leg = leg;
    }

    public Vector3 GetNextStepPos(IPivot pivot)
    {
        Vector3 nextPos = Vector3.zero;

        Ray ray = new Ray(GetNextStepPoint(pivot), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
            nextPos = raycastHit.point;

        return nextPos;
    }

    public Vector3 GetStandPos(IPivot pivot)
    {
        Vector3 standPos = Vector3.zero;

        Ray ray = new Ray(GetStandPoint(pivot), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f, GROUND_LAYER))
            standPos = raycastHit.point;

        return standPos;
    }

    private Vector3 GetNextStepPoint(IPivot pivot)
    {
        return (GetStandPoint(pivot) + (pivot.Forward * STEP_DISTANCE));
    }

    private Vector3 GetStandPoint(IPivot pivot)
    {
        return pivot.Position + (pivot.Right * (SPACING * leg.Side));
    }
}
