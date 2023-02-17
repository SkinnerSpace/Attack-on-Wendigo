using System;

public class VisionController : BaseController
{
    private VisionDetector detector;
    private event Action<VisionTarget> onUpdate;

    public override void Initialize(MainController main) => detector = new VisionDetector(main.Data.Cam, this);

    public override void Connect() => MainInputReader.Get<MousePositionInputReader>().Subscribe(detector as IMousePosObserver);

    public void Update(VisionTarget target) => onUpdate?.Invoke(target);

    public void Subscribe(IVisionObserver observer) => onUpdate += observer.OnUpdate;
    public void Unsubscribe(IVisionObserver observer) => onUpdate -= observer.OnUpdate;
}
