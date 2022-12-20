using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MovementController : IMovementController
{
    public ILegsSync legsSync { get; private set; }
    public IUnityService unityService { get; private set; }
    private ITransformProxy transform;

    public MovementController(ITransformProxy transform)
    {
        this.transform = transform;
        unityService = new UnityService();
        legsSync = new LegsSync();
    }

    public void AddLeg(ILeg leg)
    {
        legsSync.AddLeg(leg);
    }

    public void Move(float speed)
    {
        transform.Position += transform.Forward * speed * unityService.delta; 
    }
}

