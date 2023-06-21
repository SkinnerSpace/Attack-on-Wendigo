using Character;
using System;
using UnityEngine;

public class ItemInteractor : IInteractor
{
    private PlayerCharacter character;
    private CharacterData data;

    private bool isLocked;

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
        if (target != null) // Do I need to check the distance?
        {
            IInteractable interactable = target.GetComponent<IInteractable>();
            interactable.Interact(this);
        }
        else
        {
            dropAnItem();
        }
    }

    public void DropPreviouslyHeldItem()
    {
        if (!isLocked)
        {
            dropAnItem();
        }
    }
    public void TakePickableItem(IPickable pickable)
    {
        if (!isLocked)
        {
            takeAnItem(pickable);
        }
    }

    public void LockInteraction() => isLocked = true;
    public void UnlockInteraction() => isLocked = false;
}