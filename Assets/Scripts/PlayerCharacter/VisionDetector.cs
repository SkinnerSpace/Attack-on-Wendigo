using UnityEngine;

public class VisionDetector : IVisionDetector, IMousePosObserver
{
    private Camera cam;
    private VisionTarget target;

    public VisionDetector(Camera cam) => this.cam = cam;

    public VisionTarget GetTarget() => target;

    public void OnMousePosUpdate(Vector2 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Vision))
        {
            target.IsValid = true;
            target.Transform = hit.transform;
            target.distance = Vector3.Distance(ray.origin, hit.point);
        }
        else
        {
            target.IsValid = false;
        }
    }
}
