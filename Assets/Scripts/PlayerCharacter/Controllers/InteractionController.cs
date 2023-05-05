using System.Collections;
using System;
using UnityEngine;
using Character;

public class InteractionController : BaseController, IInteractionController, IMousePosObserver
{
    private const float REACH_DISTANCE = 4f;

    private PlayerCharacter main;
    private CharacterData data;
    private ItemsKeeper keeper;
    private IInputReader input;
    private VisionRaycast visionRaycast;
    private ItemInteractor itemInteractor;

    private Transform target;
    private RaycastHit targetHit;

    private FunctionTimer timer;
    private IHealthSystem healthSystem;

    private bool isLockedAfterInteraction;
    private Vector2 mousePos;

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
        HelicopterEvents.current.onBoarded += DropAnItem;

        input.Get<InteractionInputReader>().Subscribe(Interact);
        input.Get<MousePositionInputReader>().Subscribe(this);
    }

    public override void Disconnect()
    {
        input.Get<InteractionInputReader>().Unsubscribe(Interact);
        input.Get<MousePositionInputReader>().Unsubscribe(this);
    }

    public void OnMousePosUpdate(Vector2 pos)
    {
        mousePos = pos;
        targetHit = visionRaycast.Cast(pos, REACH_DISTANCE);
        Transform newTarget = targetHit.transform;

        if (target != newTarget)
            UpdateTarget(newTarget, targetHit);
    }

    private void UpdateTarget(Transform target, RaycastHit targetHit)
    {
        if (target == null)
        {
            PlayerEvents.current.RemoveInteractiveTarget();
        }

        else if (target != null)
        {
            PlayerEvents.current.AddInteractiveTarget(targetHit);
        }

        this.target = target;
    }

    public void Interact()
    {
        if (!isLockedAfterInteraction){
            isLockedAfterInteraction = true;
            itemInteractor.Interact(target);

            timer.Set("Unlock", 0.2f, () => isLockedAfterInteraction = false);
        }
    }

    public void DropAnItem() => keeper.DropAnItem(mousePos);
    public void TakeAnItem(IPickable pickable)
    {
        IWeapon weapon = pickable.Transform.GetComponentInParent<IWeapon>(); // Weapon component is usually is in the parent of an item

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
