using UnityEngine;

public class WendigoData : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float deceleration;
    [SerializeField] private float rotationSpeed;

    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed => movementSpeed;
    public float Deceleration => deceleration;
    public float RotationSpeed => rotationSpeed;
    public Vector3 Velocity { get; set; }

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }

    public Vector3 Right => transform.right;
    public Vector3 Up => transform.up;
    public Vector3 Forward => transform.forward;

    public bool IsArrived { get; set; }
    public Transform Target { get; set; }
}
