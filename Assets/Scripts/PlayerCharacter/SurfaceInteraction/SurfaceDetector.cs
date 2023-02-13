using System;
using UnityEngine;

public class SurfaceDetector : IGroundObserver
{
    private ICharacterData data;
    private ISurfaceProbeTaker probeTaker;

    private event Action<SurfaceProbe> notifyOnSurfaceFound;

    public SurfaceDetector(ICharacterData data, ISurfaceProbeTaker probeTaker)
    {
        this.data = data;
        this.probeTaker = probeTaker;
    }

    public void Subscribe(ISurfaceObserver observer) => notifyOnSurfaceFound += observer.OnSurfaceFound;
    public void Unsubscribe(ISurfaceObserver observer) => notifyOnSurfaceFound -= observer.OnSurfaceFound;

    public void OnGrounded() => Update();

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
}
