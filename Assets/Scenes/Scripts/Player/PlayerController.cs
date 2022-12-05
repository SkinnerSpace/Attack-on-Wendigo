using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    public Vector2 GetRawMovementVector()
    {
        float rawX = Input.GetAxisRaw("Horizontal");
        float rawY = Input.GetAxisRaw("Vertical");
        Vector2 rawMovementVector = new Vector2(rawX, rawY);

        return rawMovementVector;
    }

    public Vector2 GetRawCameraVector()
    {
        float rawX = Input.GetAxisRaw("Mouse X");
        float rawY = Input.GetAxisRaw("Mouse Y");
        Vector2 rawCameraVector = new Vector2(rawX, rawY);

        return rawCameraVector;
    }

    public bool JumpButtonIsPressed()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public bool HookshotButtonIsPressed()
    {
        throw new NotImplementedException();
    }
}
