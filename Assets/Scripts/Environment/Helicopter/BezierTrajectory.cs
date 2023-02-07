using System;
using UnityEngine;

[ExecuteAlways]
public class BezierTrajectory : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private BezierPointsManager pointsManager;
    [SerializeField] private BezierConstellator constellator;
    [SerializeField] private BezierLUT bezierLUT;
    [SerializeField] private BezierProjector projector;
    
    [Header("Settings")]
    [Range(3,128)]
    [SerializeField] private int details = 32;
    public float distance;

    [SerializeField] private BezierArrangement arrangement;

    public float Length => bezierLUT.arcLength;

    private void OnEnable()
    {
        pointsManager.SetUpPoints(this);
    }

    private void OnValidate() => UpdatePoints();

#if UNITY_EDITOR
    private void OnDrawGizmos() => RenderBezierCurve();
#endif

    public Vector3 GetPosition(float inDistance, Action onFinish)
    {
        float time = bezierLUT.GetTimeFromDistance(inDistance);

        if (time >= 1f){
            time = 1f;
            onFinish();
        }

        Vector3 position = projector.GetPoint(pointsManager.Points, time);
        return position;
    }

    public void UpdatePoints()
    {
        pointsManager.SetUpPoints(this);
        pointsManager.UpdatePoints();
        bezierLUT.UpdateLUT(pointsManager.Points, details);
    }

    public void RenderBezierCurve()
    {
        if (pointsManager.isReady)
        {
            float time = bezierLUT.GetTimeFromDistance(distance);
            projector.DrawPoints(pointsManager.Points, time);
            projector.DrawCurve(pointsManager.Points, details);
        }
    }

    public void GenerateTrajectory()
    {
        constellator.RearrangePoints(pointsManager.BezierPoints, arrangement);
        pointsManager.PushThePoints();
    }
}
