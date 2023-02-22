using UnityEngine;

public class HelicopterCameraPivot : MonoBehaviour
{
    [SerializeField] private Transform helicopterPoint;
    [SerializeField] private Helicopter helicopter;

    [Header("Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool testMode;

    private void Update()
    {
        Move(speed * Time.deltaTime);
        Rotate(speed * Time.deltaTime);
    }

    public void Move(float value)
    {
        Vector3 targetPos = helicopterPoint.position;
        targetPos += helicopterPoint.right * offset.x;
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

