using System.Collections.Generic;
using UnityEngine;

public class TrajectoryMarker : MonoBehaviour
{
    private const int DETAIL = 50;
    private const float DURATION = 10f;

    private LayerMask detectionLayers = 1 << 8 | 1 << 12;

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 acceleration;

    private LineRenderer line;
    private List<Vector3> drawPoints = new List<Vector3>();

    private bool drawMode;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void DrawTrajectory(Vector3 position, Vector3 velocity, Vector3 acceleration)
    {
        this.position = position;
        this.velocity = velocity;
        this.acceleration = acceleration;

        AddLine();
        DrawLine();
    }

    public void CancelTrajectory()
    {
        drawMode = false;
        line.enabled = false;
    }

    private void AddLine()
    {
        drawPoints.Clear();

        for (int i = 0; i < DETAIL; i++)
        {
            AddPoint(i);
            if (Collided(i)) return;
        }
    }

    private void DrawLine()
    {
        if (!drawMode)
        {
            drawMode = true;
            line.enabled = true;
        }

        Vector3[] linePoints = drawPoints.ToArray();
        line.positionCount = linePoints.Length;
        line.SetPositions(linePoints);
    }

    private void AddPoint(int i)
    {
        float time = GetTime(i);
        Vector3 point = GetPoint(time);
        drawPoints.Add(point);
    }

    private bool Collided(int i)
    {
        Vector3 position = drawPoints[i];
        if (Physics.CheckSphere(position, 0.05f, detectionLayers)) return true;
        return false;
    }

    private float GetTime(float i) => GetTimeUnit(i) * DURATION;
    public Vector3 GetPoint(float time) => position + VelocityIn(time) + (AdjustedAcceleration * Squared(time));

    private float GetTimeUnit(float i) => i / (DETAIL - 1f);
    private Vector3 VelocityIn(float time) => velocity * time;
    private Vector3 AdjustedAcceleration => acceleration / 2;
    private float Squared(float time) => time * time;
}

