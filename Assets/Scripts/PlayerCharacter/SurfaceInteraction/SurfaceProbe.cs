using UnityEngine;

public struct SurfaceProbe
{
    public bool isValid;
    public Surface surface;
    public Vector3 position;

    public SurfaceProbe(bool isValid, Surface surface, Vector3 position)
    {
        this.isValid = isValid;
        this.surface = surface;
        this.position = position;
    }
}
