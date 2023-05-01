using System.Collections.Generic;
using UnityEngine;

public class Trajectory
{
    private const int DETAIL = 50;
    private const float DURATION = 10f;

    private List<Vector3> points;
    private TrajectoryCollider collider;

    public Trajectory()
    {
        points = new List<Vector3>();
        collider = new TrajectoryCollider();
    }

    public void CalculateFor(ProjectedBody body)
    {
        points.Clear();

        for (int i = 0; i < DETAIL; i++)
        {
            FindPoint(body, i);
            collider.TakeAProbe(points, i);
            if (collider.hasDetected) return;
        }
    }

    private void FindPoint(ProjectedBody body, int i)
    {
        float time = GetTime(i);
        Vector3 point = GetPoint(body, time);
        points.Add(point);
    }

    private float GetTime(float i) => GetTimeUnit(i) * DURATION;
    private float GetTimeUnit(float i) => i / (DETAIL - 1f);

    public Vector3 GetPoint(ProjectedBody body, float time)
    {
        Vector3 originalPosition = body.position;
        Vector3 velocityOverTime = body.velocity * time;
        Vector3 accelerationOverTime = (body.acceleration / 2) * (time * time);
        Vector3 point = originalPosition + velocityOverTime + accelerationOverTime;

        return point;
    }

    public Vector3[] GetPoints() => points.ToArray();
    public EndPoint GetEndPoint() => collider.endPoint;
}
