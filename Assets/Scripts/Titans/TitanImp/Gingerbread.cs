using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    Vector3 targetDirection;

    public override void Update()
    {
        Move();
        FindTarget();
    }

    public override void Move()
    {
        movementController.Look(targetDirection, data.Speed);
        movementController.Move(data.Speed);

        Debug.DrawRay(transform.Position + new Vector3(0f, 10f, 0f), targetDirection, Color.red);
        Debug.DrawRay(transform.Position + new Vector3(0f, 10f, 0f), transform.Forward * 100f, Color.blue);
    }

    public override void FindTarget()
    {
        var nearest = Town.Instance.Props[PropTypes.BUILDING].
            OrderBy(t => Vector3.Distance(transform.Position, t.transform.Position)).
            FirstOrDefault();
        
        Vector3 targetPosition = nearest.transform.Position;
        targetDirection = (targetPosition - transform.Position).normalized;
    }
}

public class TargetFinder
{

}
