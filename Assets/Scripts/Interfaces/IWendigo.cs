using UnityEngine;

public interface IWendigo
{
    WendigoData Data { get; }
    void SetTarget(Transform target);
    IHitBox[] HitBoxes { get; }
}