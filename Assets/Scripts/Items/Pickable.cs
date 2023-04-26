using System;
using UnityEngine;

public class Pickable : MonoBehaviour, IPickable
{
    [SerializeField] private Transform item;
    [SerializeField] private ItemTransitionController transitionController;
    [SerializeField] private Transform physicalBodyImp;

    private IPhysicalBody physicalBody;

    public Transform Transform => item;
    public Vector3 Position => item.position;
    private Transform originalParent;
    public Transform HoldParent { get; private set; }

    private event Action onInteract;
    public event Action onPickedUp;
    public event Action onDropped;

    public bool IsReadyToHand { get; private set; } = true;

    private void Awake()
    {
        physicalBody = physicalBodyImp.GetComponent<IPhysicalBody>();
        originalParent = item.parent;
    }

    public void SubscribeOnInteraction(Action onInteract) => this.onInteract += onInteract;
    public void SubscribeOnPickedUp(Action onPickedUp) => this.onPickedUp += onPickedUp;
    public void SubscribeOnDropped(Action onDropped) => this.onDropped += onDropped;

    public void SwitchOff()
    {
        IsReadyToHand = false;
        gameObject.layer = (int)Layers.DisabledInteractables;
    }

    public void SwitchOn()
    {
        IsReadyToHand = true;
        gameObject.layer = (int)Layers.Interactables;
    }

    public void PickUp(IKeeper keeper, Action onCameToHands)
    {
        HoldParent = keeper.Root;
        item.parent = HoldParent;
        physicalBody.DisablePhysics();
        transitionController.Launch(item, onCameToHands);

        NotifyOnPickedUp();
    }

    public void Drop(Vector3 pos, Vector3 force)
    {
        item.position = pos;
        item.SetParent(originalParent);

        transitionController.Stop();
        Throw(force);

        NotifyOnDropped();
    }

    private void NotifyOnPickedUp()
    {
        onInteract?.Invoke();
        onPickedUp?.Invoke();
        GameEvents.current.WeaponHasBeenPickedUp();
    }

    private void NotifyOnDropped()
    {
        onInteract?.Invoke();
        onDropped?.Invoke();
    }

    private void Throw(Vector3 force)
    {
        physicalBody.EnablePhysics();
        physicalBody.AddForce(force);

        Vector3 torque = new Vector3(Rand.GetBisigned(), Rand.Range(-1f, 1f), Rand.GetBisigned()) * 25f;
        physicalBody.AddTorque(torque);
    }
}
