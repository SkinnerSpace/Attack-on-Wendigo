using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ILegsSync
{
    void StepIsOver();
    void AddLeg(ILeg leg);
    int GetLegsCount();
}
