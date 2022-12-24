﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    private ITransformProxy target;

    public override void Update() // => stateMachine.Tick();
    {
        //stateMachine.Tick();


        if (target == null)
            target = targetPointer.GetTarget(PropTypes.BUILDING);

        if (target != null)
            movementController.MoveTo(target);
    }
}
