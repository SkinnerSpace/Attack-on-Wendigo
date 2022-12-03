using UnityEngine;

public class CameraLook : MonoBehaviour, ICharacterDependee
{
    private Character character;
    private float cameraVerticalAngle;

    public void SetUp(Character character)
    {
        this.character = character;
        character.mainCamera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look()
    {
        Vector2 rawLook = character.controller.GetRawLook();

        character.transform.Rotate(new Vector3(0f, rawLook.x * character.data.mouseSensitivity, 0f), Space.Self);

        cameraVerticalAngle -= rawLook.y * character.data.mouseSensitivity;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);
        character.mainCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0f, 0f);
    }
}
