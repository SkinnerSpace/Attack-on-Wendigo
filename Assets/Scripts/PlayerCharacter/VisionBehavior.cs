using UnityEngine;

public class VisionBehavior : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private MainInputReader inputReader;

    private VisionController visionController;
    private VisionDetector visionDetector;

    private void Start()
    {
        visionDetector = new VisionDetector(cam);
        visionController = new VisionController(visionDetector);

        inputReader.GetInputReader<MousePositionInputReader>().Subscribe(visionDetector);
    }

    private void Update() => visionController.Update();

    public void Subscribe(IVisionObserver observer) => visionController.Subscribe(observer);
    public void Unsubscribe(IVisionObserver observer) => visionController.Unsubscribe(observer);
}