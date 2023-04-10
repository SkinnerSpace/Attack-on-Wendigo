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
        ICharacterData character = target.GetComponent<ICharacterData>();

        data.Target.Set(character, target);
        onTargetSet?.Invoke(target);
    } 

    public void ResetTarget()
    {
        onTargetSet?.Invoke(null);
    }
}