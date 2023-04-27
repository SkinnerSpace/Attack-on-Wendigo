using System;
using UnityEngine;

public class ItemGroundDetector
{
    private bool isGrounded;
    public event Action<bool> onGroundUpdate;

    public void CheckIfGrounded(Vector3 point, float radius)
    {
        if (CollidedWithTheGround(point, radius)){
            RegisterIsGrounded();
        }
        else{
            RegisterIsNotGrounded();
        }
    }

    private bool CollidedWithTheGround(Vector3 point, float radius) => Physics.CheckSphere(point, radius, ComplexLayers.Landscape);

    public void RegisterIsGrounded()
    {
        if (!isGrounded)
        {
            isGrounded = true;
            onGroundUpdate?.Invoke(isGrounded);
        }
    }

    public void RegisterIsNotGrounded()
    {
        if (isGrounded)
        {
            isGrounded = false;
            onGroundUpdate?.Invoke(isGrounded);
        }
    }
}