using UnityEngine;

public class HandPoint : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
