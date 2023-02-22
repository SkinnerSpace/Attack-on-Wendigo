using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class VerticalTuner
{
    private ICharacterData data;

    public VerticalTuner(ICharacterData data) => this.data = data;

    public float ReduceVerticalInput(float inputY)
    {
        float lerp = Mathf.Abs(data.VerticalVelocity);
        float adjustedInputY = Mathf.Lerp(inputY, 0f, lerp);

        return adjustedInputY;
    }

    public float IncreaseVerticalInput(float inputY, float verticalAdjustment)
    {
        float adjustedInputY = inputY + (data.VerticalVelocity * verticalAdjustment);

        return adjustedInputY;
    }
}
