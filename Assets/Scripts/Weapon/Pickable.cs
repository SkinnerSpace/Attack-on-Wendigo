using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private Transform item;
    [SerializeField] private ItemTransitionController transitionController;
    [SerializeField] private ItemSFXPlayer sFXPlayer;
    [SerializeField] private WeaponPhysics physics;
    [SerializeField] private WeaponSweeper sweeper;

    public Transform Transform => item;
    public Vector3 Position => item.position;
    private Transform originalParent;
    private event Action onInteract;

    public bool IsReadyToHand { get; private set; } = true;

    public void Subscribe(Action onInteract) => this.onInteract += onInteract;
    public void Unsubscribe(Action onInteract) => this.onInteract -= onInteract;


    private static int pickUpCount;
    private static event Action onFirstPickUp;
    public static void SubscribeOnFirstPickUp(Action onPickedUp) => onFirstPickUp += onPickedUp;

    public void SwitchOff()
    {
        IsReadyToHand = false;
        gameObject.layer = (int)Layers.DisabledInteractables;
        
        sweeper.SweepTheWeapon();
    }

    public void SwitchOn()
    {
        IsReadyToHand = true;
        gameObject.layer = (int)Layers.Interactables;
    }

    public void PickUp(WeaponKeeper keeper, Action onCameToHands)
    {
        originalParent = item.parent;
        item.SetParent(keeper.Root);
        SetPhysicsDisabled(true);
        sFXPlayer.PlayTakeSFX();

        transitionController.Launch(item, onCameToHands);

        onInteract?.Invoke();

        pickUpCount++;
        if (pickUpCount == 1)
            onFirstPickUp?.Invoke();
    }

    public void Drop(Vector3 pos, Vector3 force)
    {
        item.position = pos;
        item.SetParent(originalParent);

        transitionController.Stop();

        SetPhysicsDisabled(false);
        physics.AddForce(force);

        Vector3 torque = new Vector3(Rand.GetBisigned(), Rand.Range(-1f, 1f), Rand.GetBisigned()) * 25f;
        physics.AddTorque(torque);

        onInteract?.Invoke();
    }

    private void SetPhysicsDisabled(bool disabled)
    {
        physics.SetPhysicsDisabled(disabled);
        physics.SetLevitation(false);
    }
}