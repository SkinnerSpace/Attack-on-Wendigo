using System;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable
{
    [SerializeField] private Transform item;
    [SerializeField] private Rigidbody physics;
    [SerializeField] private Collider collision;
    [SerializeField] private ItemTransitionController transitionController;
    [SerializeField] private ItemSFXPlayer sFXPlayer;

    public Transform Transform => item;
    public Vector3 Position => item.position;
    private Transform originalParent;
    private event Action<bool> onPickedUp;

    public void Subscribe(IPickableObserver observer) => onPickedUp += observer.OnPickedUp;
    public void Unsubscribe(IPickableObserver observer) => onPickedUp -= observer.OnPickedUp;

    public void PickUp(IKeeper keeper, Action callback)
    {
        originalParent = item.parent;
        item.SetParent(keeper.Root);
        SetPhysicsDisabled(true);
        sFXPlayer.PlayTakeSFX();

        transitionController.Launch(item, callback);

        onPickedUp?.Invoke(true);
    }

    public void Drop(Vector3 pos, Vector3 force)
    {
        item.position = pos;
        item.SetParent(originalParent);

        transitionController.Stop();

        SetPhysicsDisabled(false);
        physics.AddForce(force);

        onPickedUp(false);
    }

    private void SetPhysicsDisabled(bool disabled)
    {
        physics.velocity = Vector3.zero;

        collision.enabled = !disabled;
        physics.isKinematic = disabled;
        physics.useGravity = !disabled;
    }
}