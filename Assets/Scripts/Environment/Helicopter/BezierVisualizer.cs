using System.Collections.Generic;
using UnityEngine;

public class BezierVisualizer : MonoBehaviour
{
    private const int MAX_DEPTH = 4;

    [Header("Required Components")]
    [SerializeField] private BezierTrajectory trajectory;
    [SerializeField] private PointsDrawer pointsDrawer;
    [SerializeField] private LinesDrawer linesDrawer;

    [Header("Settings")]
    [SerializeField] private List<BezierAppearance> apperance;
    [SerializeField] private Color curveColor = Color.black;


    public void Draw(Vector3[] points, int depth)
    {
        if (trajectory.Visualize)
        {
            int index = MAX_DEPTH - depth;

            pointsDrawer.DrawMultiple(points, apperance[index].radius, apperance[index].color);
            linesDrawer.DrawMultiple(points, apperance[index].color);
        }
    }

    public void DrawCurveSegment(Vector3 start, Vector3 end)
    {
        if (trajectory.Visualize)
        {
            linesDrawer.DrawSingle(start, end, curveColor);
        }
    }
}
