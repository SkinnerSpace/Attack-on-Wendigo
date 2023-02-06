using UnityEngine;

public class PointsDrawer : MonoBehaviour
{
    public void DrawMultiple(Vector3[] points, float radius, Color color)
    {
        foreach (Vector3 point in points)
            DrawSingle(point, radius, color);
    }

    public void DrawSingle(Vector3 position, float radius, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(position, radius);
        Gizmos.color = Color.white;
    }
}