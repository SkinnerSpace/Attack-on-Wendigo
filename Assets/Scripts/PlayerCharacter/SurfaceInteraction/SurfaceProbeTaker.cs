using UnityEngine;

public class SurfaceProbeTaker : ISurfaceProbeTaker
{
    public SurfaceProbe TakeAProbe(Ray ray)
    {
        SurfaceProbe probe = new SurfaceProbe();

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Solid))
        {
            probe.surface = hit.transform.GetComponent<ISurface>();
            probe.position = hit.point;

            if (probe.surface != null){
                probe.isValid = true;
            }
        }

        return probe;
    }
}