using UnityEngine;

public class GroundDetectorBehavior : MonoBehaviour
{
    [SerializeField] public CharacterData data;
    [SerializeField] private GroundDetector detector;
    private GroundDetectionHandler handler;

    private void Awake()
    {
        handler = new GroundDetectionHandler(data, detector);
    }

    private void Update() => handler.Update();

    public void Subscribe(IGroundObserver observer) => handler.Subscribe(observer);
    public void Unsubscribe(IGroundObserver observer) => handler.Unsubscribe(observer);
}
