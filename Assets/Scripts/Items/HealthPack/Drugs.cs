using System;
using System.Collections;
using UnityEngine;

public class Drugs : MonoBehaviour, IHandyItem, IHealthPack
{
    private static int id;

    [SerializeField] private string drugName;

    [Header("Settings")]
    [SerializeField] private int health;

    [Header("Required Components")]
    [SerializeField] private AbandonmentDetector abandonmentDetector;
    [SerializeField] private Pickable pickable;
    [SerializeField] private SwayController swayController;
    [SerializeField] private ItemSweeper sweeper;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private HealingLiquid liquid;

    private IInputReader inputReader;
    private IInteractionController interactor;
    private IHealthSystem healthSystem;

    private bool isUsed;

    private Action onReady;
    private Action onNotReady;

    public Action onInjection;

    public Vector3 DefaultPosition => new Vector3(0.75f, -0.35f, 1.5f);

    private void Awake()
    {
        AssignName();
        swayController.InitializeOnAwake(this);
    }

    private void AssignName() => transform.name = drugName + "_" + (++id);

    public void InitializeOnTake(ICharacterData characterData, IInputReader inputReader, IInteractionController interactor)
    {
        this.inputReader = inputReader;
        this.interactor = interactor;

        swayController.InitializeOnTake(characterData, inputReader);
        abandonmentDetector.Initialize(characterData);
    }

    public void SetReady(bool isReady)
    {
        if (isReady){
            onReady?.Invoke();
            inputReader.Get<CombatInputReader>().Subscribe(this);
        }
        else{
            onNotReady?.Invoke();
            inputReader.Get<CombatInputReader>().Unsubscribe(this);
        }
    }

    public void SubscribeOnReady(Action onReady) => this.onReady += onReady;

    public void SubscribeOnNotReady(Action onNotReady) => this.onNotReady += onNotReady;

    public void Use()
    {
        if (!isUsed){
            isUsed = true;
            onInjection?.Invoke();
        }
    }

    public void Apply() => healthSystem.RestoreHealth(health);

    public void SetTarget(IHealthSystem healthSystem){
        this.healthSystem = healthSystem;
    }

    public void DisposeOffUsedDrugs()
    {
        interactor.DropAnItem();
        pickable.SwitchOff();
        timer.Set("Sweep", 0.5f, () => sweeper.SweepTheWeapon());
    }

    public void Refill()
    {
        isUsed = false;
        liquid.Refill();
    }
}
