using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private PointsStorage storage;

    [Range(0, 1)]
    public float t;

    private void OnDrawGizmos()
    {
        Vector3 Pos = GetPoint(storage.positions, t);
        DrawPoint(Pos, 0.5f, Color.red);

        DrawPoints();
        DrawCurve(32, Color.white);
    }

    private void DrawCurve(int details, Color color)
    {
        Vector3 prev = storage.positions[0];
        for (int i=1; i < details; i++)
        {
            float tDraw = i / (details - 1f);
            Vector3 point = GetPoint(storage.positions, tDraw);

            if (i > 1){
                DrawLine(prev, point, color);
            }
            prev = point;
        }
    }

    private Vector3 GetPoint(Vector3[] points, float t) => points.Length >= 2 ? LerpFurther(points, t) : points[0];

    private Vector3 LerpFurther(Vector3[] points, float t)
    {
        Vector3[] lerpedPoints = new Vector3[points.Length - 1];

        for (int i = 0; i < points.Length - 1; i++){
            lerpedPoints[i] = Lerp(points[i], points[i + 1], t);
        }

        return GetPoint(lerpedPoints, t);
    }

    private void DrawPoints()
    {
        foreach (Vector3 point in storage.positions)
            DrawPoint(point, 0.5f, Color.black);
    }

    private void DrawPoint(Vector3 position, float radius, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(position, radius);
        Gizmos.color = Color.white;
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }

    Vector3 Lerp(Vector3 a, Vector3 b, float t) => a + (t * (b - a));
}
