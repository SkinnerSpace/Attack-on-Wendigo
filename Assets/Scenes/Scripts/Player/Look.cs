using UnityEngine;

public class Look : MonoBehaviour, ICommand
{
    [SerializeField] private float mouseSensitivity = 2f;

    private float horizontalAngle = 0f;
    private float verticalAngle = 0f;
    
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Execute()
    {
        HorizontalRotation();
        VerticalRotation();
    }

    private void HorizontalRotation()
    {
        horizontalAngle = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        Vector3 rotationVector = new Vector3(0f, horizontalAngle, 0f);
        transform.Rotate(rotationVector, Space.Self);
    }

    private void VerticalRotation()
    {
        verticalAngle -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalAngle = Mathf.Clamp(verticalAngle, -89f, 89f);
        mainCamera.transform.localEulerAngles = new Vector3(verticalAngle, 0f, 0f);
    }
}
