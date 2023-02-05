using UnityEngine;

public static class BezierConstraintsProjector
{
    public static void DrawConstraints(Vector3[] points, float t, BezierVisualizer visualizer)
    {
        if (points.Length >= 2) {
            LerpConstraintsFurther(points, t, visualizer);
        }
    }

    public static void LerpConstraintsFurther(Vector3[] points, float t, BezierVisualizer visualizer)
    {
        visualizer.DrawConstraintLines(points);
        Vector3[] lerpedPoints = Lerp.BetweenPoints(points, t);
        DrawConstraints(lerpedPoints, t, visualizer);
    }
}

