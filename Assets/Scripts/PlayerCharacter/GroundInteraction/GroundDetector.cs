using System;
using UnityEngine;

public class GroundDetector : BaseController, IMoverObserver
{
    private MainController main;
    private ICharacterData data;
    private IGroundDetector detector;

    private event Action notifyOnGrounded;

    public void Subscribe(IGroundObserver observer) => notifyOnGrounded += observer.Land;
    public void Unsubscribe(IGroundObserver observer) => notifyOnGrounded -= observer.Land;

    public override void Initialize(MainController main)
    {
        this.main = main;

        data = main.Data;
        detector = new GroundCollisionDetector();
    }

    public override void Connect() => main.Mover.Subscribe(this);

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
