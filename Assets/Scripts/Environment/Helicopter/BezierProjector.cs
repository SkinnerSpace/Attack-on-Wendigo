using System;
using UnityEngine;

[ExecuteAlways]
public class BezierProjector : MonoBehaviour
{
    Action<Vector3[], int> send;
    [SerializeField] private BezierVisualizer visualizer;

    private void OnEnable() => send += visualizer.Draw;
    private void OnDisable() => send -= visualizer.Draw;

    public void DrawPoints(Vector3[] points, float time) => GetPoint(points, time, send);

    public void DrawCurve(Vector3[] points, int details)
    {
        Vector3 prev = points[0];

        for (int i = 1; i < details; i++)
        {
            float tDraw = i / (details - 1f);
            Vector3 point = GetPoint(points, tDraw, null);
            visualizer.DrawCurveSegment(prev, point);
            prev = point;
        }
    }

    public Vector3 GetPoint(Vector3[] points, float time) => GetPoint(points, time, null);

    private Vector3 GetPoint(Vector3[] points, float time, Action<Vector3[], int> send)
    {
        send?.Invoke(points, points.Length);

        if (points.Length >= 2)
        {
            Vector3[] lerpedPoints = Lerp.BetweenPoints(points, time);
            return GetPoint(lerpedPoints, time, send);
        }
        return points[0];
    }
}

