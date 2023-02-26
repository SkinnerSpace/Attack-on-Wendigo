using UnityEngine;

public interface IKeeper
{
    Transform Root { get; }
    void Take(Pickable pickable, Weapon weapon);
    void DropAnItem(Vector2 screenPoint);
}
