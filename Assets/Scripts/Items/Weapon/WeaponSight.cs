using UnityEngine;

public class WeaponSight
{
    private const float MAX_SCATTER = 0.2f;
    
    private WeaponData data;
    private Camera cam;

    public WeaponSight(WeaponData data, Camera cam)
    {
        this.data = data;
        this.cam = cam;
    }

    public WeaponTarget GetTarget()
    {
        Ray ray = GetExactDirection();
        ray.direction = ApplyScatter(ray.direction);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Shootable);

        return new WeaponTarget(ray, hit);
    }

    private Vector3 ApplyScatter(Vector3 direction)
    {
        Vector3 deviation = ExtraMath.GetRandomVector3();
        float scatterDegree = Mathf.Lerp(MAX_SCATTER, 0f, data.Precision);
        Vector3 scatter = deviation * scatterDegree;
        Vector3 scatteredDirection = (direction + scatter).normalized;

        return scatteredDirection;
    }

    private Ray GetExactDirection() => cam.ScreenPointToRay(Input.mousePosition);
}
