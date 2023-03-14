using UnityEngine;


public class TransformData
{
    private Transform transform;
    public TransformData(Transform transform) => this.transform = transform;

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }
    public Vector3 Forward => transform.forward;
    public Vector3 Right => transform.right;
    public Vector3 Up => transform.up;
}
