using UnityEngine;

public class BezierPointsManager : MonoBehaviour
{
    public BezierPoint[] BezierPoints => bezierPoints;
    public Vector3[] Points => points;
    public bool isReady => points.Length > 0;

    private BezierPoint[] bezierPoints;
    private Vector3[] points;

    private Vector3 center;

    public void SetUpPoints(BezierTrajectory trajectory)
    {
        bezierPoints = GetComponentsInChildren<BezierPoint>();

        foreach (BezierPoint bezierPoint in bezierPoints)
            bezierPoint.SetTrajectory(trajectory);
    }

    public void UpdatePoints()
    {
        points = new Vector3[bezierPoints.Length];

        for (int i = 0; i < bezierPoints.Length; i++){
            points[i] = bezierPoints[i].Position;
        }
    }

    public void PushThePoints()
    {
        Vector3 meanArithmetic = bezierPoints[0].Position;

        for (int i = 1; i < bezierPoints.Length; i++)
            meanArithmetic = meanArithmetic.Average(bezierPoints[i].Position);

        center = meanArithmetic;
    }
}

