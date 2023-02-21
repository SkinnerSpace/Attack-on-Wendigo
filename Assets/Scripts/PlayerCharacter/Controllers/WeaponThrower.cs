using UnityEngine;

public class WeaponThrower
{
    private const int CLIP_OFFSET = 1;
    private const float ADJUSTMENT = 0.5f;

    private ICharacterData data;

    public WeaponThrower(ICharacterData data) => this.data = data;

    public Vector3 GetDropPos(IPickable item, Vector2 screenPoint)
    {
        if (item == null) return Vector3.zero;
        Vector3 originalPos = item.Position;

        Ray ray = data.Cam.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Vision))
        {
            if (IsClippingThroughTheWall(originalPos, ray, hit))
            {
                return GetAdjustedDropPos(ray, hit);
            }
        }

        return originalPos;
    }

    private bool IsClippingThroughTheWall(Vector3 position, Ray ray, RaycastHit hit)
    {
        Vector3 displacedHitPos = ray.origin + (ray.direction * (hit.distance - CLIP_OFFSET));
        Vector3 weaponDir = (position - displacedHitPos);
        float dot = Vector3.Dot(ray.direction, weaponDir);

        return dot > 0f;
    }

    private Vector3 GetAdjustedDropPos(Ray ray, RaycastHit hit) => ray.origin + (ray.direction * (hit.distance - ADJUSTMENT));
}
