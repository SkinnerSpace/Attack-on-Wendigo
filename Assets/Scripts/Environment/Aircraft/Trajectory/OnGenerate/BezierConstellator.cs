using UnityEngine;

public class BezierConstellator : MonoBehaviour
{
    private const float POSITIVE = 1f;
    private const float NEGATIVE = -1f;
    private const float DEVIATION_MULTIPLIER = 0.3f;

    [SerializeField] private BezierOffsetCalculator offset;

    private Vector3 straightDir;
    private Vector3 perpDir;

    public void RearrangePoints(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        ShiftStartToEnd(bezierPoints);
        ShiftEndToNew(bezierPoints, arrangement);
        CalculateStraightLine(bezierPoints);

        SetSecondPoint(bezierPoints, arrangement);
        SetThirdPoint(bezierPoints, arrangement);
    }

    private void ShiftStartToEnd(BezierPoint[] bezierPoints)
    {
        Vector3 startPos = bezierPoints[bezierPoints.Length - 1].Position;
        bezierPoints[0].SetPosition(startPos);
    }

    private void ShiftEndToNew(BezierPoint[] bezierPoints, BezierArrangement data)
    {
        Vector3 dirToPivot = (data.Pivot - bezierPoints[0].Position).normalized;
        Vector3 deviation = new Vector3(Rand.Range(-1f, 1f), 0f, Rand.Range(-1f, 1f)).normalized * DEVIATION_MULTIPLIER;
        dirToPivot = (dirToPivot + deviation).normalized;

        float length = Rand.Range(data.lineMinLength, data.lineMaxLength);
        Vector3 endPos = bezierPoints[bezierPoints.Length - 1].Position + (dirToPivot * length);
        endPos = new Vector3(endPos.x, Rand.Range(data.minHeight, data.maxHeight), endPos.z);

        bezierPoints[bezierPoints.Length - 1].SetPosition(endPos);
    }

    private void CalculateStraightLine(BezierPoint[] bezierPoints)
    {
        Vector3 vector = bezierPoints[bezierPoints.Length - 1].Position - bezierPoints[0].Position;
        straightDir = vector.normalized;
        perpDir = new Vector3(-straightDir.z, 0f, straightDir.x);
    }

    private void SetSecondPoint(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 secondStart = bezierPoints[0].Position + offset.GetStraight(arrangement, straightDir, POSITIVE);
        Vector3 secondEnd = secondStart + offset.GetPerpendicular(arrangement, perpDir);
        bezierPoints[1].SetPosition(secondEnd);
    }

    private void SetThirdPoint(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 thirdStart = bezierPoints[3].Position + offset.GetStraight(arrangement, straightDir, NEGATIVE);
        Vector3 thirdEnd = thirdStart + offset.GetPerpendicular(arrangement, perpDir);
        bezierPoints[2].SetPosition(thirdEnd);
    }
}
