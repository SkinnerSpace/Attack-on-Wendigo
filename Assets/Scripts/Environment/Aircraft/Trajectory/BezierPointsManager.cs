using UnityEngine;

public class BezierPointsManager : MonoBehaviour
{
    private const int STARTING_POINT = 0;
    private const int FREE_STARTING_POINT = 1;
    private const float PUSH_DISTANCE = 30f;

    [SerializeField] private float minDistance;

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

    public void ExtractPositionOfBezierPoints()
    {
        points = new Vector3[bezierPoints.Length];

        for (int i = STARTING_POINT; i < bezierPoints.Length; i++){
            points[i] = bezierPoints[i].Position;
        }
    }

    public void PushThePointsAwayFromEachOther()
    {
        for (int i = FREE_STARTING_POINT; i < bezierPoints.Length; i++)
        {
            Vector3 centerOfTheCurve = GetTheCenterOfTheCurve();
            Vector3 vectorFromTheCenter = bezierPoints[i].Position - centerOfTheCurve;
            Vector3 directionFromTheCenter = new Vector3(vectorFromTheCenter.x, 0f, vectorFromTheCenter.z).normalized;
            Vector3 adjustedPosition = bezierPoints[i].Position + (directionFromTheCenter * PUSH_DISTANCE);

            Vector3 vectorFromTheStartingPoint = bezierPoints[i].Position - bezierPoints[STARTING_POINT].Position;
            Vector3 directionFromTheStartingPoint = new Vector3(vectorFromTheStartingPoint.x, 0f, vectorFromTheStartingPoint.z).normalized;
            adjustedPosition += (directionFromTheStartingPoint * PUSH_DISTANCE);

            bezierPoints[i].SetPosition(adjustedPosition);
        }
    }


    public void KeepThePointsWithinTheBoundaries(Vector2 areaCenter, float radius)
    {
        bool outsideTheBoundaries = false;
        float maxViolationDistance = 0f;

        for (int i = FREE_STARTING_POINT; i < bezierPoints.Length; i++)
        {
            float distance = Vector2.Distance(bezierPoints[i].Position.FlatV2(), areaCenter);

            if (distance >= radius){
                outsideTheBoundaries = true;
                maxViolationDistance = Mathf.Max(maxViolationDistance, distance - radius);
            }
        }

        if (outsideTheBoundaries){
            AdjustPositionToTheBoundaries(areaCenter, maxViolationDistance);
        }
    }

    private void AdjustPositionToTheBoundaries(Vector3 areaCenter, float adjustmentDistance)
    {
        Vector3 centerOfTheCurve = GetTheCenterOfTheCurve().FlatV3();
        Vector3 directionToTheAreaCenter = (new Vector3(areaCenter.x, 0f, areaCenter.y) - centerOfTheCurve).normalized;

        for (int j = FREE_STARTING_POINT; j < bezierPoints.Length; j++)
        {
            Vector3 adjustedPosition = bezierPoints[j].Position + (directionToTheAreaCenter * (adjustmentDistance));
            bezierPoints[j].SetPosition(adjustedPosition);
        }
    }

    private Vector3 GetTheCenterOfTheCurve()
    {
        Vector3 centerOfTheCurve = bezierPoints[STARTING_POINT].Position;

        for (int i = STARTING_POINT; i < bezierPoints.Length; i++){
            centerOfTheCurve = centerOfTheCurve.Average(bezierPoints[i].Position);
        }

        return centerOfTheCurve;
    }
}

