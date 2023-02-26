using System.Collections;
using System;
using UnityEngine;

public class InteractionController : BaseController, IInteractor, IMousePosObserver
{
    private CharacterData data;
    private WeaponKeeper keeper;
    private IInputReader input;
    private VisionRaycast visionRaycast;
    private Transform target;

    private Pickable pickable;
    private IOpenable openable;

    private Vector2 mousePos;

    private event Action<Transform> onTargetAdded;
    private event Action<Transform> onTargetRemoved;

    public override void Initialize(MainController main)
    {
        data = main.Data;
        input = main.InputReader;

        keeper = new WeaponKeeper(data, input);
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
        mousePos = pos;
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
        {
            ActUponTarget();
        }
        else
        {
            keeper.DropAnItem(mousePos);
        }
    }

    private void ActUponTarget()
    {
        pickable = target.GetComponent<Pickable>();

        if (pickable != null)
        {
            keeper.DropAnItem(mousePos);

            Weapon weapon = target.parent.GetComponent<Weapon>();
            keeper.Take(pickable, weapon);
            return;
        }

        openable = target.GetComponent<IOpenable>();

        if (openable != null)
            openable.Open();
    }

    private bool IsCloseEnough() => Vector3.Distance(data.Position, target.position) < data.ReachDistance;
}