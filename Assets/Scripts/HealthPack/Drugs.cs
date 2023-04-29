using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drugs : MonoBehaviour, IHandyItem, IHealthPack
{
    [Header("Settings")]
    [SerializeField] private int health;

    [Header("Required Components")]
    [SerializeField] private AbandonmentDetector abandonmentDetector;
    [SerializeField] private Pickable pickable;
    [SerializeField] private ItemSweeper sweeper;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private MeshRenderer mesh;

    [Header("Materials")]
    [SerializeField] private Material fullMaterial;
    [SerializeField] private Material emptyMaterial;

    private IInputReader inputReader;
    private IInteractionController interactor;
    private IHealthSystem healthSystem;

    private bool isUsed;

    private Action onReady;

    public Vector3 DefaultPosition => new Vector3(0.75f, -0.35f, 1.5f);

    public void InitializeOnTake(ICharacterData characterData, IInputReader inputReader, IInteractionController interactor)
    {
        this.inputReader = inputReader;
        this.interactor = interactor;
        abandonmentDetector.Initialize(characterData);

    }

    public void SetReady(bool isReady)
    {
        ManageConnectionToInput(isReady);

        if (isReady){
            onReady?.Invoke();
        }
    }

    public void SubscribeOnReady(Action onReady) => this.onReady += onReady;

    public void Use()
    {
        if (!isUsed){
            isUsed = true;
            timer.Set("DropUsedDrugs", 2f, DisposeUsedDrugs);
            healthSystem.RestoreHealth(health);
        }
    }

    public void SetTarget(IHealthSystem healthSystem){
        this.healthSystem = healthSystem;
    }

    private void ManageConnectionToInput(bool connect)
    {
        if (inputReader != null)
        {
            if (connect){
                inputReader.Get<CombatInputReader>().Subscribe(this);
            }
            else{
                inputReader.Get<CombatInputReader>().Unsubscribe(this);
            }
        }
    }

    private void DisposeUsedDrugs()
    {
        mesh.material = emptyMaterial;
        interactor.DropAnItem();
        pickable.SwitchOff();
        timer.Set("Sweep", 0.5f, () => sweeper.SweepTheWeapon());
    }

    public void Refill()
    {
        isUsed = false;
    }
}
