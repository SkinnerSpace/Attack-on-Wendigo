using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FOVController
{
    // Max FOV
    // Min FOV

    private ICharacterData data;
    
    public FOVController(ICharacterData data)
    {
        this.data = data;
    }

    public void Update()
    {
        float maxVelocity = (data.Speed / data.Deceleration) * 4f;
        float power = data.FlatVelocity.magnitude / maxVelocity;
        power = Mathf.Clamp(power, 0f, 1f);
        power = Easing.QuadEaseInOut(power);
    }
}
