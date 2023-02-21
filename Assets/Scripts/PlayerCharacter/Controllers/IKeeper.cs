using UnityEngine;

public interface IKeeper
{
    Transform Root { get; }
    void TakeAnItem(IPickable item);
    void DropAnItem();
}
