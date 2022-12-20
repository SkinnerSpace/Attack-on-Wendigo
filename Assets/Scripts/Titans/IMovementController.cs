using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMovementController
{
    IUnityService unityService { get; }
    ILegsSync legsSync { get; }

    void Move(float speed);
    void AddLeg(ILeg leg);
}
