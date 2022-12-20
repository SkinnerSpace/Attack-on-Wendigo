using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UnityService : IUnityService
{
    public float delta => Time.deltaTime;

    public float GetAxisRaw(string axis)
    {
        return Input.GetAxisRaw(axis);
    }

    public float JumpPressed()
    {
        return Convert.ToInt32(Input.GetKeyDown(KeyCode.Space));
    }
}
