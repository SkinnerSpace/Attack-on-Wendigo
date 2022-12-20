using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    public override void Update()
    {
        Move();
    }

    public override void Move()
    {
        movementController.Move(data.speed);
    }
}
