using UnityEngine;

public class MockSurfaceProbeTaker : ISurfaceProbeTaker
{
    private SurfaceProbe probe;
    public void SetProbe(SurfaceProbe probe) => this.probe = probe;
    public SurfaceProbe TakeAProbe(Ray ray) => probe;
}
