using System;
using UnityEngine;

public class PlayerController : IController
{
    public Vector3 GetRawDirection()
    {
        float rawX = Input.GetAxisRaw("Horizontal");
        float rawZ = Input.GetAxisRaw("Vertical");

        return new Vector3(rawX, 0f, rawZ);
    }

    public Vector2 GetRawLook()
    {
        float rawX = Input.GetAxisRaw("Mouse X");
        float rawY = Input.GetAxisRaw("Mouse Y");
        Vector2 rawLook = new Vector2(rawX, rawY);

        return rawLook;
    }

    public bool GetJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetHookshot()
    {
        return (Input.GetKeyDown(KeyCode.E));
    }
}