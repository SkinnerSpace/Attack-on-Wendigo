using UnityEngine;

public class BezierPointsManager : MonoBehaviour
{
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

    public void DisplacePoints(Vector3 targetPivot, float minOffset, float maxOffset)
    {
        BezierPoint start = bezierPoints[0];
        BezierPoint startHandle = bezierPoints[1];
        BezierPoint endHandle = bezierPoints[2];
        BezierPoint end = bezierPoints[3];

        Vector3 startPos = end.Position;
        start.SetPosition(startPos);

        Vector3 startHandlePos = GetRandPos(targetPivot, minOffset, maxOffset);
        startHandle.SetPosition(startHandlePos);

        Vector3 endPos = GetRandPos(targetPivot, minOffset, maxOffset);
        end.SetPosition(endPos);

        Vector3 endHandlePos = GetRandPos(targetPivot, minOffset, maxOffset);
        endHandle.SetPosition(endHandlePos);
    }

    private Vector3 GetRandPos(Vector3 pivot, float minOffset, float maxOffset)
    {
        float offset = GetOffset(minOffset, maxOffset);
        Vector3 displacement = GetDisplacement(offset);
        Vector3 position = pivot + displacement;

        return position;
    }

    private float GetOffset(float minOffset, float maxOffset)
    {
        float offset = Rand.Range(-maxOffset, maxOffset);
        offset = CorrectOffset(offset, minOffset);

        return offset;
    }

    private float CorrectOffset(float offset, float minOffset) => Mathf.Abs(offset) < minOffset ? (Mathf.Sign(offset) * minOffset) : offset;

    private Vector3 GetDisplacement(float offset)
    {
        Vector3 displacement = new Vector3(
                x: Rand.Range(-offset, offset),
                y: 0f,
                z: Rand.Range(-offset, offset)
                );

        return displacement;
    }
}

