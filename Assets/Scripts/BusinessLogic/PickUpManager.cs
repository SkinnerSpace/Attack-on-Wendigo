using System;
using UnityEngine;

public static class PickUpManager
{
    private static int pickUpCount;
    private static event Action onFirstPickUp;

    public static void SubscribeOnFirstPickUp(Action onPickedUp) => onFirstPickUp += onPickedUp;

    public static void PickUp()
    {
        pickUpCount++;

        if (pickUpCount == 1){
            onFirstPickUp?.Invoke();
        }
    }
}