﻿using System.Collections;
using System;
using UnityEngine;
using Character;

public class InteractionController : BaseController, IInteractor, IMousePosObserver
{
    private const float REACH_DISTANCE = 4f;

    private PlayerCharacter main;
    private CharacterData data;
    private ItemsKeeper keeper;
    private IInputReader input;
    private VisionRaycast visionRaycast;
    private ItemInteractor itemInteractor;
    private Transform target;
    private FunctionTimer timer;
    private IHealthSystem healthSystem;

    private bool isLocked;
    private Vector2 mousePos;

    private event Action<Transform> onTargetAdded;
    private event Action<Transform> onTargetRemoved;

    public override void Initialize(PlayerCharacter main)
    {
        this.main = main;
        data = main.OldData;
        input = main.InputReader;
        timer = main.Timer;

        keeper = new ItemsKeeper(data, input, this);
        itemInteractor = ItemInteractor.CreateWithDataTakeAndDrop(main, TakeAnItem, DropAnItem);
        
        visionRaycast = new VisionRaycast(data.Cam, ComplexLayers.Interactables);
    }

    public override void Connect()
    {
        healthSystem = main.GetController<CharacterHealthSystem>();

        PlayerEvents.current.onDeath += DropAnItem;
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
        Transform newTarget = visionRaycast.Cast(pos, REACH_DISTANCE);

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
        if (!isLocked){
            isLocked = true;
            itemInteractor.Interact(target);

            timer.Set("Unlock", 0.2f, () => isLocked = false);
        }
    }

    public void DropAnItem() => keeper.DropAnItem(mousePos);
    public void TakeAnItem(IPickable pickable)
    {
        IWeapon weapon = pickable.Transform.GetComponentInParent<IWeapon>(); // IWeapon component is usually in the parent of an item

        if (weapon != null){
            keeper.TakeAWeapon(pickable, weapon);
            return;
        }

        IHealthPack healthPack = pickable.Transform.GetComponentInParent<IHealthPack>();

        if (healthPack != null){
            keeper.TakeAHealthPack(pickable, healthPack, healthSystem);
            return;
        }
    }
}
