using UnityEngine;

public class Rotator
{
    private readonly ITransformProxy transform;
    private readonly IClock clock;
    private float speed;

    private Quaternion targetRotation;

    public Rotator(ITransformProxy transform, IClock clock, float speed)
    {
        this.transform = transform;
        this.clock = clock;
        this.speed = speed;
    }

    public void RotateTo(Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - transform.Position).normalized;
        targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        transform.Rotation = Quaternion.Lerp(transform.Rotation, targetRotation, speed * clock.DeltaTime);
    }
}

