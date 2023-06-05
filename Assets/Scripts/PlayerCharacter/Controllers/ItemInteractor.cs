using Character;
using System;
using UnityEngine;

public class ItemInteractor : IInteractor
{
    private PlayerCharacter character;
    private CharacterData data;

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
        Debug.Log("Try to interact");

        if (target != null) // Do I need to check the distance?
        {
            IInteractable interactable = target.GetComponent<IInteractable>();
            interactable.Interact(this);
            Debug.Log("Target is non zero");
        }
        else
        {
            dropAnItem();
        }
    }

    public void DropPreviouslyHeldItem() => dropAnItem();
    public void TakePickableItem(IPickable pickable) => takeAnItem(pickable);  
}