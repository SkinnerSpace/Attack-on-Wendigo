using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisionInteractor : MonoBehaviour
{
    private const float REACH_DISTANCE = 4f;

    [SerializeField] private OldItemHolder weaponHolder;
    [SerializeField] private VisionTypeChecker typeChecker;

    public void InteractIfPossible(Transform item)
    {
        if (item == null) return;

        if (IsAbleToTakeAnItem(item)){
            if (Input.GetKeyDown(KeyCode.E)) weaponHolder.TakeAnItem(item);
        }
    }

    private bool IsAbleToTakeAnItem(Transform item) => IsSuitable(item) &&
                                         GetDistanceTo(item) <= REACH_DISTANCE &&
                                         typeChecker.CheckType(item) == typeof(IOldPickable);

    private bool IsSuitable(Transform targetTransform)
    {
        Type type = typeChecker.CheckType(targetTransform);
        return type != null;
    }

    private float GetDistanceTo(Transform item) => Vector3.Distance(item.position, transform.position);
}