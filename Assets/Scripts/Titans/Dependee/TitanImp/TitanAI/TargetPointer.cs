using System.Linq;
using UnityEngine;

public class TargetPointer : ITargetPointer
{
    private readonly ITransformProxy transform;

    public TargetPointer(ITransformProxy transform)
    {
        this.transform = transform;
    }

    public ITransformProxy GetTarget(PropTypes type)
    {
        var nearest = Town.Instance.Props[type].
            OrderBy(t => Vector3.Distance(transform.Position, t.transforms[0].Position)).
            FirstOrDefault();

        if (nearest != null)
            return nearest.transforms[0];

        return null;
    }
}
