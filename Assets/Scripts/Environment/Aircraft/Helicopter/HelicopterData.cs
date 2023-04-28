using UnityEngine;

public class HelicopterData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    public float Speed => speed;
    public float RotationSpeed => rotationSpeed;
}

