using UnityEngine;

public class ItemPositionSetter
{
    private Transform item;
    private Vector3 originalPos;
    private Vector3 targetPos;

    private Transform originalParent;

    public ItemPositionSetter(Transform item) => this.item = item;

    public void SetUp(IHolder holder)
    {
        originalParent = item.parent;
        item.SetParent(holder.transform, true);
        
        originalPos = item.localPosition;
        targetPos = holder.targetPosition;
    }

    public void Reset()
    {
        item.SetParent(originalParent);
    }

    public void Displace(float transition)
    {
        item.localPosition = Vector3.Lerp(originalPos, targetPos, transition);
        item.localRotation = Quaternion.Slerp(item.localRotation, Quaternion.Euler(0, 0, 0), transition);
    }
}
