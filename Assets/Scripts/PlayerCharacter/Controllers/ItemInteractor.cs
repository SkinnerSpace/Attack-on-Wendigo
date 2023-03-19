using Character;
using System;
using UnityEngine;

public class ItemInteractor
{
    private CharacterData data;

    private IPickable pickable;
    private IOpenable openable;

    private Action<IPickable, IWeapon> takeAnItem;
    private Action dropAnItem;

    public static ItemInteractor CreateWithDataTakeAndDrop(CharacterData data, Action<IPickable, IWeapon> takeAnItem, Action dropAnItem)
    {
        return new ItemInteractor(data, takeAnItem, dropAnItem);
    }

    private ItemInteractor(CharacterData data, Action<IPickable, IWeapon> takeAnItem, Action dropAnItem)
    {
        this.data = data;
        this.takeAnItem = takeAnItem;
        this.dropAnItem = dropAnItem;
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
            dropAnItem();

            IWeapon weapon = target.parent.GetComponent<IWeapon>();
            takeAnItem(pickable, weapon);
        }
    }

    private void OpenOpenable(Transform target)
    {
        openable = target.GetComponent<IOpenable>();

        if (openable != null)
            openable.Open();
    }
}