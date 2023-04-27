using UnityEngine;

public interface IItemsKeeper
{
    Transform Root { get; }
    void TakeAWeapon(IPickable pickable, IWeapon weapon);
    void DropAnItem(Vector2 screenPoint);
}

