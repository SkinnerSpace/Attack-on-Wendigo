﻿using System;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private WendigoSpawner wendigoSpawner;

    [Header("Settings")]
    [SerializeField] private bool spawnWendigoOnWeaponPickedUp;



    private void Start()
    {
        if (spawnWendigoOnWeaponPickedUp)
        {
            EventN1();
        }
    }

    private void EventN1()
    {
        //GameEvents.current.onFirstWeaponPickedUp += wendigoSpawner.Launch;
        //GameEvents.current.onFirstWeaponPickedUp
    }
}