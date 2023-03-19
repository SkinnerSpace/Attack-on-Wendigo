using UnityEngine;

public struct SurfaceProbe
{
    public bool isValid;
    public ISurface surface;
    public Vector3 position;

    public SurfaceProbe(bool isValid, ISurface surface, Vector3 position)
    {
        this.isValid = isValid;
        this.surface = surface;
        this.position = position;
    }
}
