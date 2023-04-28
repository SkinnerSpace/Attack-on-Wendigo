using Character;
using System;
using UnityEngine;

public class ItemInteractor
{
    private PlayerCharacter character;
    private CharacterData data;

    private IPickable pickable;
    private IOpenable openable;

    private Action<IPickable> takeAnItem;
    private Action dropAnItem;

    public static ItemInteractor CreateWithDataTakeAndDrop(PlayerCharacter character, Action<IPickable> takeAnItem, Action dropAnItem){
        return new ItemInteractor(character, takeAnItem, dropAnItem);
    }

    private ItemInteractor(PlayerCharacter character, Action<IPickable> takeAnItem, Action dropAnItem)
    {
        this.character = character;
        this.takeAnItem = takeAnItem;
        this.dropAnItem = dropAnItem;

        data = character.OldData;
    }

    public void Interact(Transform target)
    {
        if (target != null && IsCloseEnough(target))
        {
            PickPickable(target);
            OpenOpenable(target);
        }
        else
        {
            dropAnItem();
        }
    }

    private bool IsCloseEnough(Transform target) => Vector3.Distance(data.Position, target.position) < data.ReachDistance;

    private void PickPickable(Transform target)
    {
        pickable = target.GetComponent<IPickable>();

        if (pickable != null)
        {
            DropPreviouslyHeldItem();
            takeAnItem(pickable);
        }
    }

    private void DropPreviouslyHeldItem() => dropAnItem();

    private void OpenOpenable(Transform target)
    {
        openable = target.GetComponent<IOpenable>();

        if (openable != null)
            openable.Open();
    }
}