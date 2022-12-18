using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    public Gingerbread(TitanData data) : base(data) { }

    public override void Update()
    {
        Move();
    }

    public void Move()
    {
        movementController.Move();
    }
}
