using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IController
{
    Vector2 GetRawMovementVector();
    Vector2 GetRawCameraVector();
    bool JumpButtonIsPressed();
    bool HookshotButtonIsPressed();
}
