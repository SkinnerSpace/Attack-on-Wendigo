﻿using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform cameraHolderImp;

    [Header("Pivots")]
    [SerializeField] private Transform characterPivot;
    [SerializeField] private Transform helicopterPivot;

    private ICameraHolder cameraHolder;

    private void Start()
    {
        GameEvents.current.onGameBegun += TrackTheCharacter;
        GameEvents.current.onHelicopterIsGoingToSetOff += TrackTheHelicopterFlyingAway;
    }

    private void Awake()
    {
        cameraHolder = cameraHolderImp.GetComponent<ICameraHolder>();
    }

    public void TrackTheCharacter() => cameraHolder.SetGameMode(characterPivot);

    public void TrackTheHelicopter() => cameraHolder.SetDemoMode(helicopterPivot);
    public void TrackTheHelicopterFlyingAway() => cameraHolder.SetOutroMode(helicopterPivot);
}
