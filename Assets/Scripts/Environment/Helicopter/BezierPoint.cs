using UnityEngine;

[ExecuteAlways]
public class BezierPoint : MonoBehaviour
{
    private PointsStorage storage;
    Vector3 prevPos;

    private void OnEnable()
    {
        storage = transform.parent.GetComponent<PointsStorage>();
    }

    private void OnDrawGizmos() => NotifyOnChange();

    private void NotifyOnChange()
    {
        if (prevPos != transform.position)
            storage.UpdatePoints();
    }
}