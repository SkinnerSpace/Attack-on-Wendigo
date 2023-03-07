using UnityEngine;

public class StompHandler : BaseController, ISurfaceObserver
{
    private MainController main;
    private ICharacterData data;

    public override void Initialize(MainController main)
    {
        this.main = main;
        data = main.Data;
    }

    public override void Connect() => main.GetController<SurfaceDetector>().Subscribe(this);
    public override void Disconnect() { }

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
