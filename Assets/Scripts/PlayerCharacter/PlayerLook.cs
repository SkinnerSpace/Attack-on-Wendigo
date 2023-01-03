using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    private Transform body;
    private IKeyBinds keys;
    
    private float xRotation;
    private float yRotation;

    private void Awake()
    {
        body = player.transform;
        keys = player.transform.GetComponent<IKeyBinds>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        xRotation += InputReader.mouse.x * keys.MouseSensitivity * Time.deltaTime;

        yRotation += InputReader.mouse.y * keys.MouseSensitivity * keys.MouseInversion * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(new Vector3(yRotation, xRotation, 0f));
        body.rotation = Quaternion.Euler(new Vector3(0f, xRotation, 0f));
    }
}
