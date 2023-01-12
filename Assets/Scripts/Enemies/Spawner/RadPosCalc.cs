using UnityEngine;

public class RadPosCalc : MonoBehaviour
{
    public Vector3 GetForwardPos(Vector3 rawForwardPos, float randRadius)
    {
        return AdjustPosition(rawForwardPos, randRadius);
    }

    public Vector3 GetBackwardPos(Vector3 localPos, float randRadius, Transform viewer)
    {
        Vector3 rawBackwardPos = LocalToWorldBackward(localPos, viewer);
        return AdjustPosition(rawBackwardPos, randRadius);
    }

    public Vector3 LocalToWorldForward(Vector3 localPos, Transform viewer)
    {
        Vector3 pos = viewer.position;
        pos += localPos.x * viewer.right;
        pos += localPos.z * viewer.forward;

        return pos;
    }

    public Vector3 LocalToWorldBackward(Vector3 localPos, Transform viewer)
    {
        Vector3 pos = viewer.position;
        pos += localPos.x * -viewer.right;
        pos += localPos.z * -viewer.forward;

        return pos;
    }

    public Vector3 AdjustPosition(Vector3 pos, float adjustmentRadius)
    {
        Vector3 adjustedPos = transform.position + (pos - transform.position).normalized * adjustmentRadius;
        return adjustedPos;
    }
}
