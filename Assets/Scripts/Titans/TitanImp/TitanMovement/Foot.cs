using UnityEngine;

public class Foot
{
    public const float MIN_BEND_ANGLE = -90f;
    public const float MAX_BEND_ANGLE = -220f;
    public const float MAX_HEIGHT = 10f;

    private readonly float sign;

    public Foot(float sign)
    {
        this.sign = sign;
    }

    public void Update(ITransformProxy transform)
    {
        float heightPercent = transform.Position.y / MAX_HEIGHT;
        heightPercent = Mathf.Min(1f, heightPercent);

        float angle = Mathf.Lerp(MIN_BEND_ANGLE, MAX_BEND_ANGLE, heightPercent) * sign;
        Vector3 bendAngle = new Vector3(transform.Angle.x, transform.Angle.y, angle);

        transform.Angle = bendAngle;
    }
}
