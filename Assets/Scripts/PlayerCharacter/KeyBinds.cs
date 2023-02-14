using UnityEngine;

public class KeyBinds : MonoBehaviour, IKeyBinds
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    [Header("Mouse")]
    [Range(0f, 1000f)]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private bool mouseInversion = false;

    public float MouseSensitivity => mouseSensitivity;
    public float MouseInversion => mouseInversion ? 1f : -1f;
    
    [Header("Actions")]
    [SerializeField] private KeyCode shoot = KeyCode.Mouse0;
    [SerializeField] private KeyCode aim = KeyCode.Mouse1;
    [SerializeField] private KeyCode reload = KeyCode.R;
    [SerializeField] private KeyCode interact = KeyCode.E;

    public KeyCode Shoot => shoot;
    public KeyCode Aim => aim;
    public KeyCode Reload => reload;

    [Header("Movement")]
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveForward = KeyCode.W;
    [SerializeField] private KeyCode moveBackward = KeyCode.S;
    [SerializeField] private KeyCode jump = KeyCode.Space;
    [SerializeField] private KeyCode dash = KeyCode.LeftShift;

    public KeyCode MoveRight => moveRight;
    public KeyCode MoveLeft => moveLeft;
    public KeyCode MoveForward => moveForward;
    public KeyCode MoveBackward => moveBackward;
    public KeyCode Jump => jump;
    public KeyCode Dash => dash;
    public KeyCode Interact => interact;
}

