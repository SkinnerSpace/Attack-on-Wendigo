using System;
using UnityEngine;

public class GroundDetectorController
{
    private ICharacterData data;
    private IGroundDetector detector;

    private event Action notifyOnGrounded;

    public GroundDetectorController(ICharacterData data, IGroundDetector detector)
    {
        this.data = data;
        this.detector = detector;
    }

    public void Subscribe(IGroundObserver observer) => notifyOnGrounded += observer.OnGrounded;
    public void Unsubscribe(IGroundObserver observer) => notifyOnGrounded -= observer.OnGrounded;

    public void Update()
    {
        Vector3 detectionPosition = GetDetectionPosition(data.Position, data.GroundDetectionHeight);

        data.WasGrounded = data.IsGrounded;
        data.IsGrounded = detector.Check(detectionPosition, data.GroundDetectionRadius);
        NotifyOnChange();
    }

    public static Vector3 GetDetectionPosition(Vector3 position, float height) => position - new Vector3(0f, height, 0f);

    private void NotifyOnChange()
    {
        if ((data.WasGrounded != data.IsGrounded) && data.IsGrounded)
            notifyOnGrounded?.Invoke();
    }
}
