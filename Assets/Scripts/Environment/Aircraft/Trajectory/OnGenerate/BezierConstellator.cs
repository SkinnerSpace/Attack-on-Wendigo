using UnityEngine;

public class BezierConstellator : MonoBehaviour
{
    private const float POSITIVE = 1f;
    private const float NEGATIVE = -1f;
    private const float DEVIATION_MULTIPLIER = 0.3f;

    private const int FIRST = 0;
    private const int SECOND = 1;
    private const int THIRD = 2;
    private const int FINAL = 3;

    [SerializeField] private Transform airDispatcherImp;
    [SerializeField] private BezierOffsetCalculator offset;

    private Vector3 straightDir;
    private Vector3 perpDir;

    private IAirDispatcher airDispatcher;

    private void Awake()
    {
        airDispatcher = airDispatcherImp.GetComponent<IAirDispatcher>();
    }

    public void RearrangePoints(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        ShiftStartToEnd(bezierPoints);
        ShiftEndToNew(bezierPoints, arrangement);
        CalculateStraightLine(bezierPoints);

        SetSecondPoint(bezierPoints, arrangement);
        SetThirdPoint(bezierPoints, arrangement);
    }

    public void ArrangeThePathDownToTheGround(BezierPoint[] bezierPoints, BezierArrangement arrangement, Vector3 currentPosition)
    {
        ShiftStartToEnd(bezierPoints);
        ShiftEndToTheGround(bezierPoints, currentPosition);
        CalculateStraightLine(bezierPoints);

        SetSecondPoint(bezierPoints, arrangement);
        SetThirdPoint(bezierPoints, arrangement);
    }

    private void ShiftStartToEnd(BezierPoint[] bezierPoints)
    {
        Vector3 startPos = bezierPoints[bezierPoints.Length - 1].Position;
        bezierPoints[FIRST].SetPosition(startPos);
    }

    private void ShiftEndToNew(BezierPoint[] bezierPoints, BezierArrangement data)
    {
        Vector3 dirToPivot = (data.Pivot - bezierPoints[FIRST].Position).normalized;
        Vector3 deviation = new Vector3(Rand.Range(-1f, 1f), 0f, Rand.Range(-1f, 1f)).normalized * DEVIATION_MULTIPLIER;
        dirToPivot = (dirToPivot + deviation).normalized;

        float length = Rand.Range(data.lineMinLength, data.lineMaxLength);
        Vector3 endPos = bezierPoints[FINAL].Position + (dirToPivot * length);
        endPos = new Vector3(endPos.x, Rand.Range(data.minHeight, data.maxHeight), endPos.z);

        bezierPoints[FINAL].SetPosition(endPos);
    }

    private void ShiftEndToTheGround(BezierPoint[] bezierPoints, Vector3 currentPosition)
    {
        Vector3 landingPosition = airDispatcher.GetTheLandingPosition(currentPosition);
        bezierPoints[FINAL].SetPosition(landingPosition);
    }

    private void CalculateStraightLine(BezierPoint[] bezierPoints)
    {
        Vector3 vector = bezierPoints[FINAL].Position - bezierPoints[FIRST].Position;
        straightDir = vector.normalized;
        perpDir = new Vector3(-straightDir.z, 0f, straightDir.x);
    }

    private void SetSecondPoint(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 secondStart = bezierPoints[FIRST].Position + offset.GetStraight(arrangement, straightDir, POSITIVE);
        Vector3 secondEnd = secondStart + offset.GetPerpendicular(arrangement, perpDir);
        bezierPoints[SECOND].SetPosition(secondEnd);
    }

    private void SetThirdPoint(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 thirdStart = bezierPoints[FINAL].Position + offset.GetStraight(arrangement, straightDir, NEGATIVE);
        Vector3 thirdEnd = thirdStart + offset.GetPerpendicular(arrangement, perpDir);
        bezierPoints[THIRD].SetPosition(thirdEnd);
    }
}
