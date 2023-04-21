using System;
using UnityEngine;

public class BezierTrajectory : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private BezierPointsManager pointsManager;
    [SerializeField] private BezierConstellator constellator;
    [SerializeField] private BezierLUT bezierLUT;
    [SerializeField] private BezierProjector projector;
    
    [Header("Settings")]
    [Range(3,128)]
    [SerializeField] private int details = 32;
    [SerializeField] public float distance;
    [SerializeField] private bool visualize;

    [Header("Bounds")]
    [SerializeField] private Vector2 boundsCenter;
    [SerializeField] private float maxBoundsRadius;
    [SerializeField] private float minBoundsRadius;

    [SerializeField] private BezierArrangement arrangement;

    public float Length => bezierLUT.arcLength;
    public bool Visualize => visualize;

    private float boundsRadius;

    private void OnEnable()
    {
        pointsManager.SetUpPoints(this);
    }

    private void OnValidate() => UpdatePoints();

    private void Awake(){
        boundsRadius = maxBoundsRadius;
    }

    private void Start(){
        GameEvents.current.onDeathProgressUpdate += UpdateBoundsRadius;
    }

    public Vector3 GetPosition(float inDistance, Action onFinish)
    {
        float time = bezierLUT.GetTimeFromDistance(inDistance);

        if (time >= 1f){
            time = 1f;
            onFinish();
        }

        Vector3 position = projector.GetPoint(pointsManager.Points, time);
        return position;
    }

    public void UpdatePoints()
    {
        pointsManager.SetUpPoints(this);
        pointsManager.ExtractPositionOfBezierPoints();
        bezierLUT.UpdateLUT(pointsManager.Points, details);
    }

    public void GenerateTrajectory()
    {
        constellator.RearrangePoints(pointsManager.BezierPoints, arrangement);
        pointsManager.PushThePointsAwayFromEachOther(pointsManager.BezierPoints.Length);
        pointsManager.KeepThePointsWithinTheBoundaries(boundsCenter, boundsRadius);
    }

    public void GenerateLandingTrajectory(Vector3 currentPosition)
    {
        constellator.ArrangeThePathDownToTheGround(pointsManager.BezierPoints, arrangement, currentPosition);
        pointsManager.PushThePointsAwayFromEachOther(pointsManager.BezierPoints.Length-1);
    }

    public void UpdateBoundsRadius(float progress){
        boundsRadius = Mathf.Lerp(maxBoundsRadius, minBoundsRadius, progress);
        Debug.Log("Bounds radius " + boundsRadius);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        RenderBezierCurve();

 /*       Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(boundsCenter.x, 0f, boundsCenter.y), maxBoundsRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(boundsCenter.x, 0f, boundsCenter.y), minBoundsRadius);
        Gizmos.color = Color.white;*/
    }
#endif

    public void RenderBezierCurve()
    {
        if (pointsManager.isReady)
        {
            float time = bezierLUT.GetTimeFromDistance(distance);
            projector.DrawPoints(pointsManager.Points, time);
            projector.DrawCurve(pointsManager.Points, details);
        }
    }
}
