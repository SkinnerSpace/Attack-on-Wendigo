using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LimbData
{
    private float INJURY_THRESHOLD = 0.5f;
    private float AMPUTATION_THRESHOLD = 0.1f;

    public enum States{
        Flesh,
        Bones,
        Destroyed
    }

    [Header("Other")]
    [SerializeField] private States state = States.Flesh;
    [SerializeField] private States baldState = States.Bones;
    [SerializeField] private LimbSFXSets sFXSet = LimbSFXSets.FleshCrash;

    [SerializeField] private int healthMultiplier;
    [SerializeField] private int healthProportion;
    [SerializeField] private List<Limb> lockLimbs;
    [SerializeField] public bool canBeDestroyed;

    [Header("Test")]
    [SerializeField] private bool alwaysUnlocked;

    private int initialHealth;
    private int health;
    private float healthPercent;

    private States initialState;
    private bool initialCanBeDestroyed;
    public bool isFragile { get; set; }
    public bool isBald;

    private States previousState;
    public States State => state;

    public LimbSFXSets SFXSet => sFXSet;

    public void Initialize(){
        initialHealth = healthProportion * healthMultiplier;
        health = initialHealth;
        initialState = state;
        initialCanBeDestroyed = canBeDestroyed;
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

    public void RememberCurrentState(){
        previousState = state;
    }

    public bool StateHasBeenChanged() => previousState != state;

    public bool ReadyForMutilation()
    {
        return state == States.Flesh && 
               (healthPercent <= INJURY_THRESHOLD || isFragile)
               ;
    }

    public bool ReadyForAmputation()
    {
        return (state == States.Bones && AmputationIsUnlocked()) &&
               (AppropriateHealthForAmputation() || isFragile)
               ;
    }

    private bool AppropriateHealthForAmputation(){
        return healthPercent <= AMPUTATION_THRESHOLD &&
               canBeDestroyed;
    }

    public bool ReadyToGoBald()
    {
        return state == baldState &&
               !isBald;
    }

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

    public void ResetState()
    {
        health = initialHealth;
        healthPercent = 1f;
        state = initialState;
        canBeDestroyed = initialCanBeDestroyed;
        isFragile = false;
        isBald = false;
    }

    public void SetHealthMultiplier(int healthMultiplier) => this.healthMultiplier = healthMultiplier;
}
