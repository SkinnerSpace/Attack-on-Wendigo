using UnityEngine;

public class LinesDrawer : MonoBehaviour
{
    public void DrawMultiple(Vector3[] points, Color color)
    {
        for (int i = 0; i < points.Length - 1; i++)
            DrawSingle(points[i], points[i + 1], color);
    }

    public void DrawSingle(Vector3 start, Vector3 end, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }
}
