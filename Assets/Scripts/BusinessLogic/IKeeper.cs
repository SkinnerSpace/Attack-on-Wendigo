using UnityEngine;

public interface IKeeper
{
    Transform Root { get; }
    void Take(IPickable pickable, IWeapon weapon);
    void DropAnItem(Vector2 screenPoint);
}

