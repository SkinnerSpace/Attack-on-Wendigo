using System;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable
{
    [SerializeField] private Rigidbody physics;
    [SerializeField] private Collider collision;
    [SerializeField] private ItemTransitionController transitionController;
    [SerializeField] private ItemSFXPlayer sFXPlayer;

    public Vector3 Position => transform.position;
    private Transform originalParent;
    private event Action<bool> onPickedUp;

    public void Subscribe(IPickableObserver observer) => onPickedUp += observer.OnPickedUp;
    public void Unsubscribe(IPickableObserver observer) => onPickedUp -= observer.OnPickedUp;

    public void PickUp(IKeeper keeper, Action callback)
    {
        originalParent = transform.parent;
        transform.SetParent(keeper.Root);
        SetPhysicsDisabled(true);
        sFXPlayer.PlayTakeSFX();

        transitionController.Launch(this, callback);

        onPickedUp?.Invoke(true);
    }

    public void Drop(Vector3 pos, Vector3 force)
    {
        transform.position = pos;
        transform.SetParent(originalParent);

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

    T IPickable.Get<T>() => GetComponent<T>();
}
