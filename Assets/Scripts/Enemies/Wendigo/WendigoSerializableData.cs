using UnityEngine;

public class WendigoSerializableData : MonoBehaviour, IWendigoSerializableData
{
    [SerializeField] private int health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float deceleration;

    public int Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public float Deceleration { get { return deceleration; } set { deceleration = value; } }
}
