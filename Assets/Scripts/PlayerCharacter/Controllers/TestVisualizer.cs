using System.Collections.Generic;
using UnityEngine;

public class TestVisualizer : MonoBehaviour
{
    public static TestVisualizer Instance;
    public bool drawSphere;

    private Stack<(Vector3 pos, Color col)> points = new Stack<(Vector3 pos, Color col)>();

    private void Awake()
    {
        Instance = this;
    }

    public void DrawPoint((Vector3 pointPosition, Color color)point)
    {
        points.Push(point);
    }

    private void OnDrawGizmos()
    {
        if (points.Count > 0)
        {
            (Vector3 pos, Color col) point = points.Pop();

            Gizmos.color = point.col;
            Gizmos.DrawSphere(point.pos, 0.1f);
            Gizmos.color = Color.white;
        }
    }
}
