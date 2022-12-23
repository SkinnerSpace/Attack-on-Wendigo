using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IMovementController
{
    IUnityService UnityService { get; }
    ILegsSync LegsSync { get; }

    void Move(float speed);
    void Look(Vector3 lookDirection, float speed);
    void AddLeg(ILeg leg);
    void AddTorso(ITorso torso);
}
