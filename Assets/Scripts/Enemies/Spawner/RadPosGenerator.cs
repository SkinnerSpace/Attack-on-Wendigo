using UnityEngine;

public class RadPosGenerator : MonoBehaviour
{
    private const float HORIZON_LENGTH = 0.6f;
    private const float MAX_DISTANCE = 140;
    private const float MAX_RADIUS_OFFSET = 60f;

    [SerializeField] private float radius = 380f;
    [SerializeField] private Transform viewer;

    [SerializeField] private bool test;

    public Vector3 GetPosition()
    {
        float randPosInSight = UnityEngine.Random.Range(-HORIZON_LENGTH, HORIZON_LENGTH);
        float randRadius = radius - (UnityEngine.Random.Range(0, MAX_RADIUS_OFFSET));

        Vector3 localPos = new Vector3(randPosInSight, 0f, 1f).normalized * radius;

        Vector3 rawForwardPos = LocalToWorldForward(localPos);
        Vector3 absForwardPos = AdjustPosition(rawForwardPos, radius);
        float absDistToForwardPos = Vector3.Distance(viewer.position, absForwardPos);

        Vector3 spawnPos = absDistToForwardPos > MAX_DISTANCE
                                            ? GetForwardPos(rawForwardPos, randRadius)
                                            : GetBackwardPos(localPos, randRadius);
        return spawnPos;
    }

    private Vector3 GetForwardPos(Vector3 rawForwardPos, float randRadius)
    {
        return AdjustPosition(rawForwardPos, randRadius);
    }

    private Vector3 GetBackwardPos(Vector3 localPos, float randRadius)
    {
        Vector3 rawBackwardPos = LocalToWorldBackward(localPos);
        return AdjustPosition(rawBackwardPos, randRadius);
    }

    private Vector3 LocalToWorldForward(Vector3 localPos)
    {
        Vector3 pos = viewer.position;
        pos += localPos.x * viewer.right;
        pos += localPos.z * viewer.forward;

        return pos;
    }

    private Vector3 LocalToWorldBackward(Vector3 localPos)
    {
        Vector3 pos = viewer.position;
        pos += localPos.x * -viewer.right;
        pos += localPos.z * -viewer.forward;

        return pos;
    }

    private Vector3 AdjustPosition(Vector3 pos, float adjustmentRadius)
    {
        Vector3 adjustedPos = transform.position + (pos - transform.position).normalized * adjustmentRadius;
        return adjustedPos;
    }

    
    private void OnDrawGizmos()
    {
        if (test && viewer != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GetPosition(), 3f);
        }
    }
    
}
