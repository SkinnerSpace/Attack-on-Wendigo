using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ILegsSync
{
    ILeg CurrentLeg { get; }
    void StepIsOver();
    void Walk();
}
