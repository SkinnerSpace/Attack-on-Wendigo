using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] public float mouseSensitivity = 1f;

    private Camera playerCamera;
    private IController controller;
    private float cameraVerticalAngle;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
        controller = GetComponent<IController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look()
    {
        Vector2 rawLook = controller.GetRawLook();

        transform.Rotate(new Vector3(0f, rawLook.x * mouseSensitivity, 0f), Space.Self);
        cameraVerticalAngle -= rawLook.y * mouseSensitivity;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0f, 0f);
    }
}
