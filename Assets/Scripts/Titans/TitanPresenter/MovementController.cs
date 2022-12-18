using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MovementController
{
    public ILegsSync legsSync;
    private TransformProxy transform;

    public MovementController(ILegsSync legsSync, TransformProxy transform)
    {
        this.legsSync = legsSync;
        this.transform = transform;
    }

    public void Move()
    {
        transform.position += new Vector3(1f, 0f, 0f);
    }
}

