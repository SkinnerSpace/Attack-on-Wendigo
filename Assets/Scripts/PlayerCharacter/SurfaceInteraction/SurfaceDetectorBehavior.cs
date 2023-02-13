using UnityEngine;

public class SurfaceDetectorBehavior : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public CharacterData data;
    [SerializeField] private SurfaceProbeTaker probeTaker;

    [Header("Subjects")]
    [SerializeField] private GroundDetectorBehavior groundDetectorSubject;

    private SurfaceDetector surfaceDetector;

    private void Awake()
    {
        surfaceDetector = new SurfaceDetector(data, probeTaker);
    }

    private void Start()
    {
        groundDetectorSubject.Subscribe(surfaceDetector);
    }

    public void Subscribe(ISurfaceObserver observer) => surfaceDetector.Subscribe(observer);
    public void Unsubscribe(ISurfaceObserver observer) => surfaceDetector.Unsubscribe(observer);
}
