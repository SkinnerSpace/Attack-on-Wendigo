using UnityEngine;

public class DirectionReader
{
    public bool right { get; private set; }
    public bool left { get; private set; }
    public bool forward { get; private set; }
    public bool backward { get; private set; }

    public Vector3 rawDir { get; private set; }
    public Vector3 normDir { get; private set; }

    private IKeyBinds keys;

    public DirectionReader(IKeyBinds keys) => this.keys = keys; 

    public void Read()
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
}
