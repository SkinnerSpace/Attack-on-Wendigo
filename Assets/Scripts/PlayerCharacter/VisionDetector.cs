using UnityEngine;

public class VisionDetector : IVisionDetector, IMousePosObserver
{
    private Camera cam;
    private VisionTarget target;

    public VisionDetector(Camera cam)
    {
        this.cam = cam;
    }

    public VisionTarget GetTarget() => target;

    public void OnMousePosUpdate(Vector2 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Vision))
        {
            target.isValid = true;
            target.name = hit.transform.name;
            // CALCULATE DISTANCE
            // FIND TYPE
            // ALL OTHER STUFF

            // Vision observers must decide for themeselves whether the target is appropriate to them
            // Probably I must create new class responsible for that
        }
        else
        {
            target.isValid = false;
        }
    }
}
