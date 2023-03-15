using System;
using UnityEngine;

public class SurfaceDetector : BaseController, IGroundObserver
{
    private PlayerCharacter main;
    private ICharacterData data;
    private ISurfaceProbeTaker probeTaker;

    private event Action<SurfaceProbe> notifyOnSurfaceFound;

    public void Subscribe(ISurfaceObserver observer) => notifyOnSurfaceFound += observer.OnSurfaceFound;
    public void Unsubscribe(ISurfaceObserver observer) => notifyOnSurfaceFound -= observer.OnSurfaceFound;

    public void Land() => Update();

    public void Update()
    {
        SurfaceProbe probe = ProbeTheSurface();

        if (probe.isValid) 
            notifyOnSurfaceFound?.Invoke(probe);
    }

    public SurfaceProbe ProbeTheSurface()
    {
        Vector3 position = GetRayPosition(data.Position, data.Height);
        Ray ray = new Ray(position, Vector3.down);
        SurfaceProbe probe = probeTaker.TakeAProbe(ray);

        return probe;
    }

    public static Vector3 GetRayPosition(Vector3 position, float height) => position - new Vector3(0f, height / 2f, 0f);

    public override void Initialize(PlayerCharacter main)
    {
        this.main = main;
        data = main.Data;
        probeTaker = new SurfaceProbeTaker();
    }

    public override void Connect() => main.GetController<GroundDetector>().Subscribe(this);
    public override void Disconnect() { }
}
