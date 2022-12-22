using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMovementController
{
    IUnityService UnityService { get; }
    ILegsSync LegsSync { get; }

    void Move(float speed);
    void AddLeg(ILeg leg);
    void AddTorso(ITorso torso);
}
