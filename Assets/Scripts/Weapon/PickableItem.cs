using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [SerializeField] private ItemBehaviorController behaviorController;
    [SerializeField] private ItemTransitionController transitionController;
    [SerializeField] private ItemSFXPlayer sFXPlayer;

    private ItemPositionSetter positionSetter;

    private void Awake() => positionSetter = new ItemPositionSetter(transform);

    public void PickUp(IKeeper keeper, Action onPickedUp)
    {
        behaviorController.GetReadyToHands();
        positionSetter.SetUp(keeper);
        transitionController.Launch(positionSetter, onPickedUp);
        sFXPlayer.PlayTakeSFX();
    }

    public void Drop(Vector3 force)
    {
        behaviorController.ThrowAway(force);
        positionSetter.Reset();
    }
}
