using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private const float STEEP_ANGLE = 70f;

    [SerializeField] private PlayerCharacter player;
    private Transform body;
    private IKeyBinds keys;
    
    private float xAngle;
    private float yAngle;

    private void Awake()
    {
        InitializeComponents();
        SetUpCursor();
    }

    private void InitializeComponents()
    {
        body = player.transform;
        keys = OldInputReader.Instance.keys;
    }

    private void SetUpCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() => Look();

    private void Look()
    {
        xAngle = ModifyAngleX(xAngle);
        yAngle = ModifyAngleY(yAngle);

        SetCameraRotation();
        SetBodyRotation(); 
    }

    private float ModifyAngleX(float xAngle) => xAngle + (OldInputReader.mouse.x * keys.MouseSensitivity * Time.deltaTime);

    private float ModifyAngleY(float yAngle)
    {
        float yRotated = yAngle;
        yRotated += OldInputReader.mouse.y * keys.MouseSensitivity * keys.MouseInversion * Time.deltaTime;
        yRotated = Mathf.Clamp(yRotated, -90, 90f);

        return yRotated;
    }

    private void SetCameraRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            x: yAngle, 
            y: xAngle, 
            z: transform.eulerAngles.z));
    }

    private void SetBodyRotation()
    {
        body.rotation = Quaternion.Euler(new Vector3(
            x: 0f, 
            y: xAngle, 
            z: 0f));
    }

    public bool IsLookingDown()
    {
        return yAngle >= STEEP_ANGLE;
    }
}
