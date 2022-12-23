using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IMovementController
{
    ILegsSync LegsSync { get; }
    IClock clock { get; }

    void Move(float speed);
    void AddLeg(ILeg leg);
    void AddTorso(ITorso torso);
}
