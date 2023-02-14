using UnityEngine;

public class SurfaceStompHandler : ISurfaceObserver
{
    private ICharacterData data;

    public SurfaceStompHandler(ICharacterData data) => this.data = data;

    public void OnSurfaceFound(SurfaceProbe probe) => HitTheSurface(probe);

    private void HitTheSurface(SurfaceProbe probe)
    {
        Surface surface = probe.surface;
        Vector3 position = probe.position;
        Vector3 direction = data.FlatVelocity.normalized;

        surface.Hit().
                WithPosition(position).
                WithAngle(direction, Vector3.up).
                WithShape(1f, 70f).
                WithCount(5, 10).
                Launch();
    }
}
