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
        SetIntermediatePoints(bezierPoints, arrangement);
    }

    public void ArrangeThePathDownToTheGround(BezierPoint[] bezierPoints, BezierArrangement arrangement, Vector3 currentPosition)
    {
        ShiftStartToEnd(bezierPoints);
        ShiftEndToTheLandingPosition(bezierPoints, arrangement, currentPosition);
        SetIntermediatePoints(bezierPoints, arrangement);
    }

    public void ArrangeTheEscapePath(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        ShiftStartToEnd(bezierPoints);
        ShiftEndAway(bezierPoints);
        SetIntermediatePointsWithOffsetMultipliers(bezierPoints, arrangement);
    }

    private void ShiftStartToEnd(BezierPoint[] bezierPoints)
    {
        Vector3 startPos = bezierPoints[bezierPoints.Length - 1].Position;
        bezierPoints[FIRST].SetPosition(startPos);
    }

    private void ShiftEndToNew(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 pivotOffset = GetPivotOffset(bezierPoints, arrangement);
        Vector3 endPos = bezierPoints[FINAL].Position + pivotOffset;
        endPos = SetRandomHeight(endPos, arrangement);

        bezierPoints[FINAL].SetPosition(endPos);
    }

    private void ShiftEndToTheLandingPosition(BezierPoint[] bezierPoints, BezierArrangement arrangement, Vector3 currentPosition)
    {
        InitializeAirDispatcherIfNecessary();
        Vector3 landingPosition = airDispatcher.GetTheLandingPosition(currentPosition, arrangement.minHeight);
        bezierPoints[FINAL].SetPosition(landingPosition);
    }

    public void ShiftEndDown(BezierPoint[] bezierPoints, float landHeight)
    {
        Vector3 endPos = bezierPoints[FINAL].Position;
        endPos = new Vector3(endPos.x, landHeight, endPos.z);
        bezierPoints[FINAL].SetPosition(endPos);
    }

    private void ShiftEndAway(BezierPoint[] bezierPoints)
    {
        InitializeAirDispatcherIfNecessary();
        Vector3 endPos = airDispatcher.GetEscapePosition();
        bezierPoints[FINAL].SetPosition(endPos);
    }

    private void InitializeAirDispatcherIfNecessary()
    {
        if (airDispatcher == null){
            airDispatcher = airDispatcherImp.GetComponent<IAirDispatcher>();
        }
    }

    private Vector3 SetRandomHeight(Vector3 position, BezierArrangement data)
    {
        float height = Rand.Range(data.minHeight, data.maxHeight);
        Vector3 modifiedPosition = new Vector3(position.x, height, position.z);

        return modifiedPosition;
    }

    private Vector3 GetPivotOffset(BezierPoint[] bezierPoints, BezierArrangement data)
    {
        Vector3 dirToPivot = GetRandomPivotDirection(bezierPoints, data);
        float length = Rand.Range(data.lineMinLength, data.lineMaxLength);
        Vector3 pivotOffset = dirToPivot * length;

        return pivotOffset;
    }

    private Vector3 GetRandomPivotDirection(BezierPoint[] bezierPoints, BezierArrangement data)
    {
        Vector3 dirToPivot = (data.Pivot - bezierPoints[FIRST].Position).normalized;
        Vector3 deviation = new Vector3(Rand.Range(-1f, 1f), 0f, Rand.Range(-1f, 1f)).normalized * DEVIATION_MULTIPLIER;
        dirToPivot = (dirToPivot + deviation).normalized;

        return dirToPivot;
    }

    private void SetIntermediatePoints(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        CalculateStraightLine(bezierPoints);
        SetSecondPoint(bezierPoints, arrangement);
        SetThirdPoint(bezierPoints, arrangement);
    }

    private void SetIntermediatePointsWithOffsetMultipliers(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        CalculateStraightLine(bezierPoints);
        SetSecondPointWithOffsetMultiplier(bezierPoints, arrangement);
        SetThirdPointWithOffsetMultiplier(bezierPoints, arrangement);
    }

    private void CalculateStraightLine(BezierPoint[] bezierPoints)
    {
        Vector3 vector = bezierPoints[FINAL].Position - bezierPoints[FIRST].Position;
        straightDir = vector.normalized;
        perpDir = new Vector3(-straightDir.z, 0f, straightDir.x);
    }

    private void SetSecondPoint(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 straightOffset = offset.GetStraight(arrangement, straightDir, POSITIVE);
        Vector3 secondStart = bezierPoints[FIRST].Position + straightOffset;

        Vector3 perpendicularOffset = offset.GetPerpendicular(arrangement, perpDir);
        Vector3 secondEnd = secondStart + perpendicularOffset;

        bezierPoints[SECOND].SetPosition(secondEnd);
    }

    private void SetSecondPointWithOffsetMultiplier(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 straightOffset = offset.GetStraight(arrangement, straightDir, POSITIVE) * arrangement.straightOffsetMultiplier;
        Vector3 secondStart = bezierPoints[FIRST].Position + straightOffset;

        Vector3 perpendicularOffset = offset.GetPerpendicular(arrangement, perpDir) * arrangement.perpendicularOffsetMultiplier;
        Vector3 secondEnd = secondStart + perpendicularOffset;

        bezierPoints[SECOND].SetPosition(secondEnd);
    }

    private void SetThirdPoint(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 straightOffset = offset.GetStraight(arrangement, straightDir, NEGATIVE);
        Vector3 thirdStart = bezierPoints[FINAL].Position + straightOffset;

        Vector3 perpendicularOffset = offset.GetPerpendicular(arrangement, perpDir);
        Vector3 thirdEnd = thirdStart + perpendicularOffset;

        bezierPoints[THIRD].SetPosition(thirdEnd);
    }

    private void SetThirdPointWithOffsetMultiplier(BezierPoint[] bezierPoints, BezierArrangement arrangement)
    {
        Vector3 straightOffset = offset.GetStraight(arrangement, straightDir, NEGATIVE) * arrangement.straightOffsetMultiplier; 
        Vector3 thirdStart = bezierPoints[FINAL].Position + straightOffset;

        Vector3 perpendicularOffset = offset.GetPerpendicular(arrangement, perpDir) * arrangement.perpendicularOffsetMultiplier;
        Vector3 thirdEnd = thirdStart + perpendicularOffset;

        bezierPoints[THIRD].SetPosition(thirdEnd);
    }

    public Vector3 GetEndPointPosition(BezierPoint[] bezierPoints) => bezierPoints[FINAL].Position;
}
