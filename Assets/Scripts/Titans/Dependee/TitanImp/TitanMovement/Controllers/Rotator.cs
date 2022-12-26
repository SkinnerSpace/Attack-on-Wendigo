using UnityEngine;

public class Rotator
{
    private readonly ITransformProxy transform;
    private readonly IClock clock;
    private float speed;

    public Rotator(ITransformProxy transform, IClock clock, float speed)
    {
        this.transform = transform;
        this.clock = clock;
        this.speed = speed;
    }

    public void RotateTo(Vector3 targetPosition)
    {
        Debug.DrawRay(targetPosition, new Vector3(0f, 100f, 0f), Color.red);

        Vector3 direction = CalculateDirection(targetPosition);

        Debug.DrawRay(transform.Position + new Vector3(0f, 20f, 0f), direction * 100f, Color.red);

        Vector3 targetAngle = CalculateAngle(direction);
        transform.Angle = Vector3.Lerp(transform.Angle, targetAngle, speed * clock.Delta);

        Debug.Log("Current " + transform.Angle + " expected " + targetAngle);
    }

    public Vector3 CalculateDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.Position).normalized;
        return direction;
    }

    public Vector3 CalculateAngle(Vector3 direction)
    {
        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 targetAngle = new Vector3(0f, angleY, 0f);

        return targetAngle;
    }
}

