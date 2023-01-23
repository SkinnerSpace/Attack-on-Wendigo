using UnityEngine;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance { get; private set; }

    public static bool right { get; private set; }
    public static bool left { get; private set; }
    public static bool forward { get; private set; }
    public static bool backward { get; private set; }

    public static Vector3 rawDir { get; private set; }
    public static Vector3 normDir { get; private set; }
    public static bool jump { get; private set; }
    public static bool dash { get; private set; }
    public static Vector2 mouse { get; private set; }

    public static bool leftClick { get; private set; }
    public static bool rightClick { get; private set; }

    private IKeyBinds keys;

    private InputReader() { }

    private void Awake()
    {
        PreserveSingleton();
        keys = GetComponent<IKeyBinds>();
    }

    private void PreserveSingleton()
    {
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }

    private void Update()
    {
        ReadDirectionInput();
        ReadJumpInput();
        ReadDashInput();
        ReadMouseInput();
    }

    private void ReadDirectionInput()
    {
        ReadMoveButtons();

        rawDir = GetRawDirection();
        normDir = rawDir.normalized;
    }

    private void ReadMoveButtons()
    {
        right = Input.GetKey(keys.MoveRight);
        left = Input.GetKey(keys.MoveLeft);
        forward = Input.GetKey(keys.MoveForward);
        backward = Input.GetKey(keys.MoveBackward);
    }

    private Vector3 GetRawDirection() => new Vector3(GetXAxis(), 0f, GetZAxis());

    private float GetXAxis()
    {
        float moveRight = right ? 1f : 0f;
        float moveLeft = left ? -1f : 0f;
        return moveRight + moveLeft;
    }

    private float GetZAxis()
    {
        float moveForward = forward ? 1f : 0f;
        float moveBackward = backward ? -1f : 0f;
        return moveForward + moveBackward;
    }

    private void ReadJumpInput() => jump = Input.GetKey(keys.Jump);
    private void ReadDashInput() => dash = Input.GetKeyDown(keys.Dash);

    private void ReadMouseInput()
    {
        ReadMousePosition();
        ReadMouseClicks();
    }

    private void ReadMousePosition()
    {
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y");

        mouse = new Vector2(xAxis, yAxis);
    }

    private void ReadMouseClicks()
    {
        leftClick = Input.GetMouseButton(0);
        rightClick = Input.GetMouseButton(1);
    }
}
