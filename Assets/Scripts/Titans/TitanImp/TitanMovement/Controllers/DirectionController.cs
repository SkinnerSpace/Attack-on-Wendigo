using UnityEngine;

public class DirectionController : IDirectionController
{
    private readonly ITransformProxy transform;
    private readonly IClock clock;

    public DirectionController(ITransformProxy transform, IClock clock)
    {
        this.transform = transform;
        this.clock = clock;
    }

    public void LookAt(Vector3 targetPosition, float speed)
    {
        Vector3 direction = targetPosition - transform.Position;
        Vector3 targetAngle = CalculateAngle(direction);
        transform.Angle = Vector3.Lerp(transform.Angle, targetAngle, speed * clock.Delta);
    }

    public Vector3 CalculateAngle(Vector3 direction)
    {
        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 targetAngle = new Vector3(0f, angleY, 0f);

        return targetAngle;
    }
}

