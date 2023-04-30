using UnityEngine;

public class HelicopterModel : MonoBehaviour
{
    [SerializeField] private Transform newParent;
    [SerializeField] private Transform anchor;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private void Awake()
    {
        transform.SetParent(newParent);
    }

    private void Start()
    {
        HelicopterEvents.current.onMovedForTheFirstTime += MoveInstantly;
    }

    private void LateUpdate()
    {
        MoveSmoothly();
    }

    private void MoveSmoothly()
    {
        transform.position = Vector3.Lerp(transform.position, anchor.position, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, anchor.rotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveInstantly()
    {
        transform.position = anchor.position;
        transform.rotation = anchor.rotation;
    }
}
