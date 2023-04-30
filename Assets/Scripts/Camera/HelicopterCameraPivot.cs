using UnityEngine;

public class HelicopterCameraPivot : MonoBehaviour
{
    private const float TRACK_SPEED_ADJUSTMENT = 2.5f;

    [SerializeField] private Transform helicopterPoint;
    [SerializeField] private Camera cam;

    [Header("Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool testMode;

    private float speedMultiplier;

    private float targetPosition;
    private float halfWidth;
    private float difference;

    private void Start(){
        HelicopterEvents.current.onMovedForTheFirstTime += SetInstantly;
    }

    private void Update()
    {
        AdjustTrackSpeed();
        Move(speed * speedMultiplier * Time.deltaTime);
        Rotate(speed * speedMultiplier * Time.deltaTime);
    }

    private void AdjustTrackSpeed()
    {
        targetPosition = cam.WorldToScreenPoint(helicopterPoint.position).x;
        halfWidth = Screen.width / 2f;
        difference = Mathf.Abs(halfWidth - targetPosition) / halfWidth;
        difference = Mathf.Clamp01(difference);

        speedMultiplier = 1f + (Easing.QuadEaseIn(difference) * TRACK_SPEED_ADJUSTMENT);
    }

    public void Move(float value)
    {
        Vector3 targetPos = helicopterPoint.position;
        targetPos += (helicopterPoint.right * -1f) * offset.x;
        targetPos += helicopterPoint.up * offset.y;
        targetPos += helicopterPoint.forward * offset.z;

        transform.position = Vector3.Lerp(transform.position, targetPos, value);
    }

    public void Rotate(float value)
    {
        Quaternion lookRotation = Quaternion.LookRotation((helicopterPoint.position - transform.position).normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, value);
    }

    public void SetInstantly()
    {
        Move(1f);
        Rotate(1f);
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (testMode && helicopterPoint != null)
        {
            Move(speed * Time.deltaTime);
            Rotate(speed * Time.deltaTime);
        }
    }
# endif
}

