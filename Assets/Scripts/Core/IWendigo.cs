using UnityEngine;
using WendigoCharacter;

public interface IWendigo
{
    WendigoData Data { get; }
    Animator Animator { get; }
    IChronos Chronos { get; }
    void SetTarget(Transform target);
    IHitBox[] HitBoxes { get; }
}