using System;
using UnityEngine;

public class VisionDetector : BaseController, IVisionDetector, IMousePosObserver
{
    private Camera cam;
    private VisionTarget target;
    private IInputReader input;

    public event Action<VisionTarget> onTargetUpdate;

    public override void Initialize(MainController main)
    {
        cam = main.Data.Cam;
        input = main.InputReader;
    }

    public override void Connect() => input.Get<MousePositionInputReader>().Subscribe(this);

    public void AddObserver(IVisionObserver observer) => onTargetUpdate += observer.OnTargetUpdate;
    public void Unsubscribe(IVisionObserver observer) => onTargetUpdate -= observer.OnTargetUpdate;

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

        NotifyOnUpdate(target);
    }

    public void NotifyOnUpdate(VisionTarget target) => onTargetUpdate?.Invoke(target);
}
