using System;
using UnityEngine;

public class UnityService : IUnityService
{
    public float Delta => Time.deltaTime;

    public float GetAxisRaw(string axis)
    {
        return Input.GetAxisRaw(axis);
    }

    public float JumpPressed()
    {
        return Convert.ToInt32(Input.GetKeyDown(KeyCode.Space));
    }
}
