using UnityEngine;

public class WendigoData 
{
    private IWendigoSerializableData serializable;
    private ITransform transform;

    public WendigoData(IWendigoSerializableData serializable, ITransform transform)
    {
        this.serializable = serializable;
        this.transform = transform;
    }

    public int Health { get { return serializable.Health; } set { serializable.Health = value; } }
    public float MovementSpeed => serializable.MovementSpeed;
    public float Deceleration => serializable.Deceleration;
    public float RotationSpeed => serializable.RotationSpeed;
    public Vector3 Velocity { get; set; }

    public Vector3 Position { get { return transform.Position; } set { transform.Position = value; } }
    public Quaternion Rotation { get { return transform.Rotation; } set { transform.Rotation = value; } }

    public Vector3 Right => transform.Right;
    public Vector3 Up => transform.Up;
    public Vector3 Forward => transform.Forward;

    public bool IsActive { get; set; }
    public bool IsArrived { get; set; }
    public Transform Target { get; set; }
}

