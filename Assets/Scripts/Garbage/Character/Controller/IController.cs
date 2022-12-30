using UnityEngine;

public interface IController
{
    Vector3 GetRawDirection();
    Vector2 GetRawLook();
    bool GetJump();
    bool GetHookshot();
}