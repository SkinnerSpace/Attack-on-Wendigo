using UnityEngine;

public struct SurfaceHitCollider
{
    public Vector3 direction;
    public float radius;

    public SurfaceHitCollider(Vector3 direction, float radius)
    {
        this.direction = direction;
        this.radius = radius;
    }
}
