using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierTrajectory : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private PointsStorage storage;
    [SerializeField] private BezierVisualizer visualizer;

    [SerializeField] private BezierLUT bezierLUT;
    [SerializeField] private BezierCurveProjector curveProjector;

    [Range(0, 1)]
    private float time;

    public float distance;

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        visualizer.DrawHandlePoints(storage.Points);

        time = bezierLUT.GetTimeFromDistance(distance);
        Vector3 curvePos = BezierPointProjector.GetPoint(storage.Points, time);
        visualizer.DrawPoint(curvePos, 0.5f, Color.red);

        if (storage.IsReady)
        {
            //DrawPositionPoint();
            DrawCurve();
            DrawConstraints();
            //visualizer.DrawPoints(LUT, 0.5f, Color.green);
        }
    }

    private void DrawPositionPoint() => BezierProjector.DrawPoint(storage.Points, time, visualizer);
    private void DrawCurve() => curveProjector.DrawCurve(storage.Points, visualizer);
    private void DrawConstraints() => BezierProjector.DrawConstraints(storage.Points, time, visualizer);

    #endif
}


