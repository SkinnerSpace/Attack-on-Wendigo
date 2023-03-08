using System;
using UnityEngine;

public class ItemInteractor
{
    private CharacterData data;

    private Pickable pickable;
    private IOpenable openable;

    private Action<Pickable, Weapon> takeAnItem;
    private Action dropAnItem;

    public static ItemInteractor CreateWithDataTakeAndDrop(CharacterData data, Action<Pickable, Weapon> takeAnItem, Action dropAnItem)
    {
        return new ItemInteractor(data, takeAnItem, dropAnItem);
    }

    private ItemInteractor(CharacterData data, Action<Pickable, Weapon> takeAnItem, Action dropAnItem)
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
        pickable = target.GetComponent<Pickable>();

        if (pickable != null)
        {
            dropAnItem();

            Weapon weapon = target.parent.GetComponent<Weapon>();
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