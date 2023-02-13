using UnityEngine;

public class SurfaceProbeTaker : MonoBehaviour, ISurfaceProbeTaker
{
    public SurfaceProbe TakeAProbe(Ray ray)
    {
        SurfaceProbe probe = new SurfaceProbe();

        if (Physics.Raycast(ray, out RaycastHit hit, ComplexLayers.Solid))
        {
            probe.surface = hit.transform.GetComponent<Surface>();
            probe.position = hit.point;

            if (probe.surface != null)
                probe.isValid = true;
        }

        return probe;
    }
}