using UnityEngine;

public class BezierPointsManager : MonoBehaviour
{
    public BezierPoint[] BezierPoints => bezierPoints;
    public Vector3[] Points => points;
    public bool isReady => points.Length > 0;

    private BezierPoint[] bezierPoints;
    private Vector3[] points;

    public void SetUpPoints(BezierTrajectory trajectory)
    {
        bezierPoints = GetComponentsInChildren<BezierPoint>();

        foreach (BezierPoint bezierPoint in bezierPoints)
            bezierPoint.SetTrajectory(trajectory);
    }

    public void UpdatePoints()
    {
        points = new Vector3[bezierPoints.Length];

        for (int i = 0; i < bezierPoints.Length; i++)
        {
            points[i] = bezierPoints[i].Position;
        }
    }

    /*public void DisplacePoints(Vector3 pivot, BezierDisplacement dispData)
    {
        ShiftStartToExEnd();
        SetRandomPos(pivot, dispData);
    }

    private void ShiftStartToExEnd()
    {
        Vector3 startPos = bezierPoints[bezierPoints.Length - 1].Position;
        bezierPoints[0].SetPosition(startPos);
    }

    private void SetRandomPos(Vector3 pivot, BezierDisplacement dispData)
    {
        for (int i = 1; i < bezierPoints.Length; i++)
        {
            Vector3 randomPos = GetRandPos(pivot, dispData);
            bezierPoints[i].SetPosition(randomPos);
        }
    }

    private Vector3 GetRandPos(Vector3 pivot, BezierDisplacement dispData)
    {
        float offsetRange = GetOffsetRange(dispData.minOffset, dispData.maxOffset);
        float height = GetHeight(dispData.minHeight, dispData.maxHeight);
        Vector3 displacement = GetDisplacement(offsetRange, height);
        Vector3 position = pivot + displacement;

        return position;
    }

    private float GetOffsetRange(float minOffset, float maxOffset)
    {
        float offset = Rand.Range(-maxOffset, maxOffset);
        offset = CorrectOffsetRange(offset, minOffset);

        return offset;
    }

    private float CorrectOffsetRange(float offset, float minOffset) => Mathf.Abs(offset) < minOffset ? (Mathf.Sign(offset) * minOffset) : offset;

    private float GetHeight(float minHeight, float maxHeight) => Rand.Range(minHeight, maxHeight);

    private Vector3 GetDisplacement(float offsetRange, float height)
    {
        Vector3 displacement = new Vector3(
                x: Rand.Range(-offsetRange, offsetRange),
                y: height,
                z: Rand.Range(-offsetRange, offsetRange)
                );

        return displacement;
    }*/
}

