using UnityEngine;

public class LegRaycaster
{
    private const int GROUND_LAYER = 1 << 8;

    private Leg leg;
    ITransformProxy titanTransform;

    private float stepDistance;
    private float spacing;

    public LegRaycaster(Leg leg, LegSetupPack setupPack)
    {
        this.leg = leg;
        stepDistance = setupPack.stepDistance;
        spacing = setupPack.spacing;
    }

    public Vector3 GetNextStepPos()
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

    private Vector3 GetNextStepPoint()
    {
        return (GetStandPoint() + (titanTransform.Forward * stepDistance));
    }

    private Vector3 GetStandPoint()
    {
        return titanTransform.Position + (titanTransform.Right * (spacing * leg.Side));
    }
}
