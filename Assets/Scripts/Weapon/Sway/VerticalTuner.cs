using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class VerticalTuner
{
    public static float ReduceVerticalInput(float inputY)
    {
        float lerp = Mathf.Abs(PlayerVerticalMovement.velocityMagnitude);
        float adjustedInputY = Mathf.Lerp(inputY, 0f, lerp);

        return adjustedInputY;
    }

    public static float IncreaseVerticalInput(float inputY, float verticalAdjustment, float landAdjustment)
    {
        float adjustedInputY = inputY + 
            (PlayerVerticalMovement.velocityMagnitude * verticalAdjustment) + 
            (PlayerVerticalMovement.landMagnitude);
        
        return adjustedInputY;
    }
}
