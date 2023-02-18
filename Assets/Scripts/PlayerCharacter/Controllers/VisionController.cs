using System;

public class VisionController : BaseController
{
    private IVisionDetector detector;
    private event Action<VisionTarget> onUpdate;

    public override void Initialize(MainController main) { }
    public void SetDetector(IVisionDetector detector) => this.detector = detector;

    public override void Connect() => MainInputReader.Get<MousePositionInputReader>().Subscribe(detector as IMousePosObserver);

    public void OnTargetUpdate(VisionTarget target) => onUpdate?.Invoke(target);

    public void Subscribe(IVisionObserver observer) => onUpdate += observer.OnTargetUpdate;
    public void Unsubscribe(IVisionObserver observer) => onUpdate -= observer.OnTargetUpdate;
}
