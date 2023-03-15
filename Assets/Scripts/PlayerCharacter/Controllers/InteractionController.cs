using System.Collections;
using System;
using UnityEngine;

public class InteractionController : BaseController, IInteractor, IMousePosObserver
{
    private PlayerCharacter main;
    private CharacterData data;
    private WeaponKeeper keeper;
    private IInputReader input;
    private VisionRaycast visionRaycast;
    private ItemInteractor itemInteractor;
    private Transform target;

    private Vector2 mousePos;

    private event Action<Transform> onTargetAdded;
    private event Action<Transform> onTargetRemoved;

    public override void Initialize(PlayerCharacter main)
    {
        this.main = main;
        data = main.Data;
        input = main.InputReader;

        keeper = new WeaponKeeper(data, input);
        itemInteractor = ItemInteractor.CreateWithDataTakeAndDrop(data, TakeAnItem, DropAnItem);
        
        visionRaycast = new VisionRaycast(data.Cam, ComplexLayers.Interactables);
    }

    public override void Connect()
    {
        main.GetController<CharacterHealthSystem>().SubscribeOnDeath(DropAnItem);
        input.Get<InteractionInputReader>().Subscribe(Interact);
        input.Get<MousePositionInputReader>().Subscribe(this);
    }

    public override void Disconnect()
    {
        input.Get<InteractionInputReader>().Unsubscribe(Interact);
        input.Get<MousePositionInputReader>().Unsubscribe(this);
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

    public void Interact() => itemInteractor.Interact(target);

    private void DropAnItem() => keeper.DropAnItem(mousePos);
    private void TakeAnItem(Pickable pickable, Weapon weapon) => keeper.Take(pickable, weapon);
}
