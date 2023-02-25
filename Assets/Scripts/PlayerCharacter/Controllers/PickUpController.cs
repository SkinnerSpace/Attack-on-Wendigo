using System.Collections;
using System;
using UnityEngine;

public class PickUpController : BaseController, IInteractor, IMousePosObserver
{
    private CharacterData data;
    private WeaponKeeper keeper;
    private IInputReader input;
    private VisionRaycast visionRaycast;
    private Transform target;

    private event Action<Transform> onTargetAdded;
    private event Action<Transform> onTargetRemoved;

    public override void Initialize(MainController main)
    {
        data = main.Data;
        keeper = main.GetController<WeaponKeeper>();
        input = main.InputReader;
        visionRaycast = new VisionRaycast(data.Cam, ComplexLayers.Interactables);
    }

    public override void Connect()
    {
        input.Get<InteractionInputReader>().Subscribe(this);
        input.Get<MousePositionInputReader>().Subscribe(this);
    }

    public void Subscribe(Action<Transform> addTarget, Action<Transform> removeTarget)
    {
        onTargetAdded += addTarget;
        onTargetRemoved += removeTarget;
    }

    public void OnMousePosUpdate(Vector2 pos)
    {
        Transform newTarget = visionRaycast.Cast(pos);

        if (target != newTarget)
            UpdateTarget(newTarget);
    }

    private void UpdateTarget(Transform target)
    {
        if (target == null)
            onTargetRemoved(this.target);

        else if (target != null)
            onTargetAdded(target);

        this.target = target;
    }

    public void Interact()
    {
        if (target != null && IsCloseEnough())
            keeper.TakeAnItem(target);
    }

    private bool IsCloseEnough() => Vector3.Distance(data.Position, target.position) < data.ReachDistance;
}

// When interact with an interactble
// Pickable pulls keeper and calls a take an item method
// Crate just opens
// Use polymophism!