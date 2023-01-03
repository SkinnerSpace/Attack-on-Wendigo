using UnityEngine;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance { get; private set; }

    public static Vector3 rawDir { get; private set; }
    public static Vector3 normDir { get; private set; }
    public static bool jump { get; private set; }
    public static Vector2 mouse { get; private set; }

    private IKeyBinds keys;

    private InputReader() { }

    private void Awake()
    {
        PreserveSingleton();
        keys = GetComponent<IKeyBinds>();
    }

    private void PreserveSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        ReadDirectionInput();
        ReadJumpInput();
        ReadMouseInput();
    }

    private void ReadDirectionInput()
    {
        float moveRight = Input.GetKey(keys.MoveRight) ? 1f : 0f;
        float moveLeft = Input.GetKey(keys.MoveLeft) ? -1f : 0f;
        float xAxis = moveRight + moveLeft;

        float moveForward = Input.GetKey(keys.MoveForward) ? 1f : 0f;
        float moveBackward = Input.GetKey(keys.MoveBackward) ? -1f : 0f;
        float zAxis = moveForward + moveBackward;

        rawDir = new Vector3(xAxis, 0f, zAxis);
        normDir = rawDir.normalized;
    }

    private void ReadJumpInput()
    {
        jump = Input.GetKey(keys.Jump);
    }

    private void ReadMouseInput()
    {
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y");

        mouse = new Vector2(xAxis, yAxis);
    }
}
