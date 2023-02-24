using System;
using UnityEngine;

public class WendigoTargetManager
{
    private WendigoData data;
    private event Action<Transform> onTargetSet;

    public void Subscribe(Action<Transform> onTargetSet) => this.onTargetSet += onTargetSet;

    public WendigoTargetManager(Wendigo wendigo)
    {
        data = wendigo.Data;
    }

    public void SetTarget(Transform target)
    {
        data.Target = target;
        onTargetSet?.Invoke(target);
    }
}