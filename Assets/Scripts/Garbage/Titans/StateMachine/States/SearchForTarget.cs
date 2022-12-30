using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SearchForTarget : IState
{
    private readonly Titan titan;
    private readonly ITargetPointer targetPointer;

    public SearchForTarget(Titan titan, ITargetPointer targetPointer)
    {
        this.titan = titan;
        this.targetPointer = targetPointer;
    }

    public void Tick()
    {
        //titan.Target = targetPointer.GetTarget(PropTypes.BUILDING);
    }

    public void OnEnter()
    {
        Debug.Log("Search");
    }

    public void OnExit() { }
}
