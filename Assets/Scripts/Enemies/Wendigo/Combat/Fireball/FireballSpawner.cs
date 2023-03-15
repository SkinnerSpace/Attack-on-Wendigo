using UnityEngine;
using WendigoCharacter;

public class FireballSpawner
{
    private FireballSpawnerData data;

    public float XPositiveMaxAngle => data.VerticalAngle / 2f;
    public float XNegativeMaxAngle => 360f - XPositiveMaxAngle;

    public float YPositiveMaxAngle => data.HorizontalAngle / 2f;
    public float YNegativeMaxAngle => 360f - YPositiveMaxAngle;

    public FireballSpawner(FireballSpawnerData data) => this.data = data;

    public Vector3 GetConstrainedAngles(Vector3 angles)
    {
        float xAngle = GetConstrainedXAngle(angles.x);
        float yAngle = GetConstrainedYAngle(angles.y);

        Vector3 constrainedAngles = new Vector3(xAngle, yAngle, 0f);
        return constrainedAngles;
    }

    public float GetConstrainedXAngle(float xAngle)
    {
        if (xAngle < 180f && xAngle >= XPositiveMaxAngle)
            xAngle = XPositiveMaxAngle;

        if (xAngle > 180f && xAngle <= XNegativeMaxAngle)
            xAngle = XNegativeMaxAngle;

        return xAngle;
    }

    public float GetConstrainedYAngle(float yAngle)
    {
        if (yAngle < 180f && yAngle >= YPositiveMaxAngle)
            yAngle = YPositiveMaxAngle;

        if (yAngle > 180f && yAngle <= YNegativeMaxAngle)
            yAngle = YNegativeMaxAngle;

        return yAngle;
    }
}