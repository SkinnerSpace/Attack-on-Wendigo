using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    Vector3 destination;
    Vector3 direction;

    public override void Update()
    {
        Move();
        FindTarget();

        Debug.DrawRay(transform.Position + new Vector3(0f, 10f, 0f), direction * 100f, Color.red);
    }

    public override void Move()
    {
        movementController.Move(data.Speed);
    }

    public override void FindTarget()
    {
        var nearest = Town.Instance.Props[PropTypes.BUILDING].
            OrderBy(t => Vector3.Distance(transform.Position, t.transform.Position)).
            FirstOrDefault();

        destination = nearest.transform.Position;
        direction = destination - transform.Position;
    }
}
