using UnityEngine;

public class VisionRaycast
{
    private const float BACK_SIDE_DISTANCE_MULTIPLIER = 1.5f;

    private Camera cam;
    private LayerMask layerMask;

    public VisionRaycast(Camera cam, LayerMask layerMask)
    {
        this.cam = cam;
        this.layerMask = layerMask;
    }

    public RaycastHit Cast(Vector2 pos, float distance)
    {
        Ray ray = cam.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit directHit, distance, layerMask)){
            return directHit;
        }

        return TryToGetATargetFromTheBackSide(ray, distance);
    }

    private RaycastHit TryToGetATargetFromTheBackSide(Ray ray, float distance)
    {
        float increasedDistance = distance * BACK_SIDE_DISTANCE_MULTIPLIER;

        Ray reversedRay = ray;
        reversedRay.direction = (reversedRay.direction * -1f) * increasedDistance;
        reversedRay.origin = ray.origin + (ray.direction * increasedDistance);

        Physics.Raycast(reversedRay, out RaycastHit reversedHit, increasedDistance, layerMask);

        return reversedHit;
    }
}
