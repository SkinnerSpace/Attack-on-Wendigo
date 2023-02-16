using System;

public class VisionController
{
    private IVisionDetector detector;
    private event Action<VisionTarget> onUpdate;

    public VisionController(IVisionDetector detector)
    {
        this.detector = detector;
    }

    public void Update()
    {
        VisionTarget target = detector.GetTarget();
        onUpdate?.Invoke(target);
    }

    public void Subscribe(IVisionObserver observer) => onUpdate += observer.OnUpdate;
    public void Unsubscribe(IVisionObserver observer) => onUpdate -= observer.OnUpdate;
}
