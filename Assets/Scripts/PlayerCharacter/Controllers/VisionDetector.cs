using UnityEngine;

public class VisionDetector : IMousePosObserver
{
    private Camera cam;
    private VisionController visionController;
    private VisionTarget target;

    public VisionDetector(Camera cam, VisionController visionController)
    {
        this.cam = cam;
        this.visionController = visionController;
    }

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

        visionController.Update(target);
    }
}
