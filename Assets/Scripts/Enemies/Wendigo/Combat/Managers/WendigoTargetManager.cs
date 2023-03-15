using System;
using UnityEngine;
using WendigoCharacter;

public class WendigoTargetManager : WendigoPlugableComponent
{
    private WendigoData data;
    private event Action<Transform> onTargetSet;

    public void Subscribe(Action<Transform> onTargetSet) => this.onTargetSet += onTargetSet;

    public override void Initialize(Wendigo wendigo) => data = wendigo.Data;

    public void SetTarget(Transform target)
    {
        data.Target = new WendigoTarget(target);
        onTargetSet?.Invoke(target);
    } 
}