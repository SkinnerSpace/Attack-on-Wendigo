﻿using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LimbData
{
    private float INJURY_THRESHOLD = 0.5f;
    private float AMPUTATION_THRESHOLD = 0.1f;

    private enum States{
        Flesh,
        Bones,
        Destroyed
    }

    [SerializeField] private States state = States.Flesh;
    [SerializeField] private States baldState = States.Bones;
    [SerializeField] private LimbSFXSets sFXSet = LimbSFXSets.FleshCrash;

    [SerializeField] private int initialHealth;
    [SerializeField] private List<Limb> lockLimbs;
    [SerializeField] public bool canBeDestroyed;

    [Header("Test")]
    [SerializeField] private bool alwaysUnlocked;

    private int health;
    private float healthPercent;

    public LimbSFXSets SFXSet => sFXSet;

    public void Initialize(){
        health = initialHealth;
    }

    public void SubtractHealth(int damage)
    {
        if (health > 0){
            health -= damage;
            health = Mathf.Max(health, 0);
        }

        healthPercent = Mathf.InverseLerp(0f, initialHealth, health);
    }

    public void SetStateToInjured() => state = States.Bones;
    public void SetStateToDestroyed() => state = States.Destroyed;

    public bool ReadyForMutilation()
    {
        return healthPercent <= INJURY_THRESHOLD &&
               state == States.Flesh
               ;
    }

    public bool ReadyForAmputation()
    {
        return healthPercent <= AMPUTATION_THRESHOLD &&
               state == States.Bones &&
               canBeDestroyed &&
               AmputationIsUnlocked()
               ;
    }

    public bool ReadyToGoBald() => state == baldState;

    private bool AmputationIsUnlocked()
    {
        if (alwaysUnlocked)
            return true;

        if (lockLimbs != null){
            foreach (Limb limb in lockLimbs){
                if (!limb.IsDestroyed())
                    return false;
            }
        }

        return true;
    }

    public bool IsHealthy() => state == States.Flesh;
    public bool IsInjured() => state == States.Bones;
    public bool IsDestroyed() => state == States.Destroyed;
}
