using UnityEngine;

public struct SurfaceProbe
{
    public Surface surface;
    public Vector3 position;

    public SurfaceProbe(Surface surface, Vector3 position)
    {
        this.surface = surface;
        this.position = position;
    }
}
