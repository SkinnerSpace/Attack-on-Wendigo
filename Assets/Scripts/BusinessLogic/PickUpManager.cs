using System;
using UnityEngine;

public class PickUpManager
{
    private static int pickUpCount;
    private static event Action onFirstPickUp;

    public static PickUpManager Instance{
        get{
            if (instance == null){
                instance = new PickUpManager();
            }

            return instance;
        }
    }

    private static PickUpManager instance;

    private PickUpManager() { }

    public void ResetState()
    {
        pickUpCount = 0;
        onFirstPickUp = null;
    }

    public void SubscribeOnFirstPickUp(Action onPickedUp) => onFirstPickUp += onPickedUp;

    public void PickUp()
    {
        pickUpCount++;

        if (pickUpCount == 1){
            onFirstPickUp?.Invoke();
        }
    }
}