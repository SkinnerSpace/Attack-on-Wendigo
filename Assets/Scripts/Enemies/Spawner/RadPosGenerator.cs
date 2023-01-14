using UnityEngine;

public class RadPosGenerator : MonoBehaviour
{
    
    private const float MAX_DISTANCE = 140;
    private const float MAX_RADIUS_OFFSET = 60f;

    [SerializeField] private RadPosCalc radPosCalc;
    [SerializeField] private Transform viewer;

    [SerializeField] private float radius = 380f;
    [SerializeField] private float horizonLength = 0.6f;
    [SerializeField] private bool test;

    public Vector3 GetPosition()
    {
        float randPosInSight = UnityEngine.Random.Range(-horizonLength, horizonLength);
        float randRadius = radius - (UnityEngine.Random.Range(0, MAX_RADIUS_OFFSET));

        Vector3 localPos = new Vector3(randPosInSight, 0f, 1f).normalized * radius;

        Vector3 rawForwardPos = radPosCalc.LocalToWorldForward(localPos, viewer);
        Vector3 absForwardPos = radPosCalc.AdjustPosition(rawForwardPos, radius);
        float absDistToForwardPos = Vector3.Distance(viewer.position, absForwardPos);

        Vector3 spawnPos = absDistToForwardPos > MAX_DISTANCE
                                            ? radPosCalc.GetForwardPos(rawForwardPos, randRadius)
                                            : radPosCalc.GetBackwardPos(localPos, randRadius, viewer);
        return spawnPos;
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
