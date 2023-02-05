using UnityEngine;

public static class BezierProjector
{
    public static void DrawPoint(Vector3[] points, float position, BezierVisualizer visualizer){
        BezierPointProjector.DrawPoint(points, position, visualizer);
    }

    public static void DrawConstraints(Vector3[] points, float position, BezierVisualizer visualizer){
        BezierConstraintsProjector.DrawConstraints(points, position, visualizer);
    }
}
