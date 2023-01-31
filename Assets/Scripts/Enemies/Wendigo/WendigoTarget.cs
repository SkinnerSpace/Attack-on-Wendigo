using UnityEngine;

public class WendigoTarget
{
    public bool Exist => target != null;
    public Vector3 Position => target.position;
    private Transform target;

    public void Set(Transform target) => this.target = target;
}
