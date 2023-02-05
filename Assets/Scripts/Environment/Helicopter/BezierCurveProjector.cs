using UnityEngine;

public static class BezierCurveProjector
{
    public static void DrawCurve(Vector3[] points, BezierVisualizer visualizer)
    {
        Vector3 prev = points[0];

        for (int i = 1; i < visualizer.Details; i++)
        {
            float tDraw = i / (visualizer.Details - 1f);
            Vector3 point = BezierPointProjector.GetPoint(points, tDraw);
            visualizer.DrawCurveSegment(prev, point);
            prev = point;
        }
    }
}

