using UnityEngine;

public class Foot
{
    private const float DEFAULT_ANGLE = -90f;
    private const float MAX_BEND_ANGLE = -220f;
    private const float MAX_HEIGHT = 10f;

    private readonly float sign;

    public Foot(float sign)
    {
        this.sign = sign;
    }

    public void Update(ITransformProxy transform)
    {
        float heightPercent = transform.Position.y / MAX_HEIGHT;
        heightPercent = Mathf.Min(1f, heightPercent);

        float angle = Mathf.Lerp(DEFAULT_ANGLE, MAX_BEND_ANGLE, heightPercent) * sign;
        Vector3 bendAngle = new Vector3(transform.Angle.x, transform.Angle.y, angle);

        transform.Angle = bendAngle;
    }
}
