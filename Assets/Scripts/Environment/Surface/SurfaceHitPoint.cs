using UnityEngine;

public struct SurfaceHitPoint
{
    public Vector3 position;
    public Vector3 normal;

    public SurfaceHitPoint(Vector3 position, Vector3 normal)
    {
        this.position = position;
        this.normal = normal;
    }
}
