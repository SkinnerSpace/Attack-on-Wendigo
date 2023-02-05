using UnityEngine;

public static class BezierPointProjector
{
    public static void DrawPoint(Vector3[] points, float position, BezierVisualizer visualizer)
    {
        Vector3 point = GetPoint(points, position);
        visualizer.DrawPositionPoint(point);
    }

    public static Vector3 GetPoint(Vector3[] Points, float t) => Points.Length >= 2 ? LerpFurther(Points, t) : Points[0];

    public static Vector3 LerpFurther(Vector3[] points, float t)
    {
        Vector3[] lerpedPoints = Lerp.BetweenPoints(points, t);
        return GetPoint(lerpedPoints, t);
    }
}

