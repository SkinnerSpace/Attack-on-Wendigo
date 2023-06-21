using System;
using System.Collections;
using UnityEngine;

public class Drugs : MonoBehaviour, IHandyItem, IHealthPack
{
    [Header("Settings")]
    [SerializeField] private int health;

    [Header("Required Components")]
    [SerializeField] private AbandonmentDetector abandonmentDetector;
    [SerializeField] private Pickable pickable;
    [SerializeField] private SwayController swayController;
    [SerializeField] private ItemSweeper sweeper;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private HealingLiquid liquid;
    [SerializeField] private DrugsSFXPlayer sFXPlayer;

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
        swayController.InitializeOnAwake(this);
    }

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
            pickable.SetReady();
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
            interactor.LockInteractions();
            isUsed = true;
            onInjection?.Invoke();
            sFXPlayer.PlayInjectionSFX();
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
        interactor.UnlockInteractions();
    }

    public void Refill()
    {
        isUsed = false;
        liquid.Refill();
    }
}
