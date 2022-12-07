using UnityEngine;

public class Look : Command
{
    private Vector2 rawCameraVector;
    private float horizontalAngle = 0f;
    private float verticalAngle = 0f;

    private CharacterData data;
    private Transform body;
    private IController controller;

    private void Awake()
    {
        data = transform.parent.GetComponent<CharacterData>();
        body = transform.parent.transform;
        controller = transform.parent.GetComponent<IController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Execute()
    {
        rawCameraVector = controller.GetRawCameraVector();
        HorizontalRotation();
        VerticalRotation();
    }

    private void HorizontalRotation()
    {
        horizontalAngle = rawCameraVector.x * data.mouseSensitivity;
        Vector3 rotationVector = new Vector3(0f, horizontalAngle, 0f);
        body.Rotate(rotationVector, Space.Self);
    }

    private void VerticalRotation()
    {
        verticalAngle -= rawCameraVector.y * data.mouseSensitivity;
        verticalAngle = Mathf.Clamp(verticalAngle, -89f, 89f);
        transform.localEulerAngles = new Vector3(verticalAngle, 0f, 0f);
    }
}
