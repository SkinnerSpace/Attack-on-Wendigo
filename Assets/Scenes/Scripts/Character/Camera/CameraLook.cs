using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Character character;

    private Camera playerCamera;
    private float cameraVerticalAngle;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look()
    {
        Vector2 rawLook = character.controller.GetRawLook();

        character.transform.Rotate(new Vector3(0f, rawLook.x * character.data.mouseSensitivity, 0f), Space.Self);

        cameraVerticalAngle -= rawLook.y * character.data.mouseSensitivity;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0f, 0f);
    }
}
