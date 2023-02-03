using UnityEngine;

public class WeaponTarget
{
    public Surface surface { get; private set; }
    public IDamageable damageable { get; private set; }
    public Vector3 hitPosition { get; private set; }
    public Vector3 hitDirection { get; private set; }
    public Vector3 normal { get; private set; }
    public string name { get; private set; }

    public bool exist;
    public bool isDamageable;

    public WeaponTarget(Ray ray, RaycastHit hit)
    {
        if (hit.transform != null){
            exist = true;
            name = hit.transform.name;

            surface = hit.transform.GetComponent<Surface>();
            damageable = hit.transform.GetComponent<IDamageable>();
            isDamageable = damageable != null;

            hitPosition = hit.point;
            normal = hit.normal;
        }

        hitDirection = ray.direction;
    }
}
