using UnityEngine;

public class BezierVisualizer : MonoBehaviour
{
    public int Details => curveDetails; 

    [Header("Config")]
    [SerializeField] private int curveDetails = 32;
    [SerializeField] private float handlePointsSize = 0.5f;
    [SerializeField] private float positionPointSize = 1f;

    [Header("Colors")]
    [SerializeField] private Color constraintLinesColor = Color.white;
    [SerializeField] private Color handlePointsColor = Color.green;
    [SerializeField] private Color curveColor = Color.black;
    [SerializeField] private Color pointColor = Color.red;

    public void DrawConstraintLines(Vector3[] points) => DrawLines(points, constraintLinesColor);
    public void DrawHandlePoints(Vector3[] points) => DrawPoints(points, handlePointsSize, handlePointsColor);
    public void DrawCurveSegment(Vector3 start, Vector3 end) => DrawLine(start, end, curveColor);
    public void DrawPositionPoint(Vector3 point) => DrawPoint(point, positionPointSize, pointColor);

    public void DrawPoints(Vector3[] points, float radius, Color color)
    {
        foreach (Vector3 point in points)
            DrawPoint(point, radius, color);
    }

    public void DrawPoint(Vector3 position, float radius, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(position, radius);
        Gizmos.color = Color.white;
    }

    private void DrawLines(Vector3[] points, Color color)
    {
        for (int i = 0; i < points.Length - 1; i++){
            DrawLine(points[i], points[i + 1], color);
        }
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }
}