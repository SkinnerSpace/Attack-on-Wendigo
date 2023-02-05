using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTrajectory : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private PointsStorage storage;
    [SerializeField] private BezierVisualizer visualizer;

    [Range(0, 1)]
    public float position;

    private void OnDrawGizmos()
    {
        visualizer.DrawHandlePoints(storage.Points);

        if (storage.IsReady)
        {
            DrawPositionPoint();
            DrawCurve();
            DrawConstraints();
        }
    }

    private void DrawPositionPoint() => BezierProjector.DrawPoint(storage.Points, position, visualizer);
    private void DrawCurve() => BezierProjector.DrawCurve(storage.Points, visualizer);
    private void DrawConstraints() => BezierProjector.DrawConstraints(storage.Points, position, visualizer);
}

