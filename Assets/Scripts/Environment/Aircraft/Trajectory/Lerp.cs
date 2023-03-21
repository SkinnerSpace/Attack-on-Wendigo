using UnityEngine;

public static class Lerp
{
    public static Vector3[] BetweenPoints(Vector3[] points, float t)
    {
        Vector3[] lerpedPoints = new Vector3[points.Length - 1];

        for (int i = 0; i < points.Length - 1; i++)
        {
            lerpedPoints[i] = Point(points[i], points[i + 1], t);
        }

        return lerpedPoints;
    }

    public static Vector3 Point(Vector3 a, Vector3 b, float t) => a + (t * (b - a));
}

