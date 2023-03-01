using UnityEngine;

public class WendigoData 
{
    private IWendigoSerializableData serializable;
    private Transform transform;

    public WendigoData(IWendigoSerializableData serializable, Transform transform)
    {
        this.serializable = serializable;
        this.transform = transform;
    }

    public int Health { get { return serializable.Health; } set { serializable.Health = value; } }
    public bool IsAlive => Health > 0f;

    public float MovementSpeed => serializable.MovementSpeed;
    public float Deceleration => serializable.Deceleration;
    public float RotationSpeed => serializable.RotationSpeed;
    public Vector3 Velocity { get; set; }

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }

    public Vector3 Right => transform.right;
    public Vector3 Up => transform.up;
    public Vector3 Forward => transform.forward;

    public bool IsActive { get; set; }
    public bool IsArrived { get; set; }
    public Transform Target { get { return serializable.Target; } set { serializable.Target = value; } }
}

