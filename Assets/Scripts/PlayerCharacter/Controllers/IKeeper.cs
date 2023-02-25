using UnityEngine;

public interface IKeeper
{
    Transform Root { get; }
    void TakeAnItem(Transform item);
    void DropAnItem();
}
