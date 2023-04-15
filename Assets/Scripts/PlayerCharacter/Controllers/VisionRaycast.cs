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

    public Transform Cast(Vector2 pos, float distance)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Transform currentTarget = null;

        if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask)){
            currentTarget = hit.transform;
        }

        if (currentTarget == null){
            currentTarget = TryToGetATargetFromTheBackSide(ray, distance);
        }

        return currentTarget;
    }

    private Transform TryToGetATargetFromTheBackSide(Ray ray, float distance)
    {
        float increasedDistance = distance * BACK_SIDE_DISTANCE_MULTIPLIER;

        Ray reversedRay = ray;
        reversedRay.direction = (reversedRay.direction * -1f) * increasedDistance;
        reversedRay.origin = ray.origin + (ray.direction * increasedDistance);

        if (Physics.Raycast(reversedRay, out RaycastHit reversedHit, increasedDistance, layerMask)){
            return reversedHit.transform;
        }

        return null;
    }
}
