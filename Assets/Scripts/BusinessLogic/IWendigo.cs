using UnityEngine;

public interface IWendigo
{
    Animator Animator { get; }
    IChronos Chronos { get; }
    void SetTarget(Transform target);
    //IHitBox[] HitBoxes { get; }
}