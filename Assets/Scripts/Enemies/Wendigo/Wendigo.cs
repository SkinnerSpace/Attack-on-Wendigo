using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo : MonoBehaviour, IWendigo, IRagdoll, IPooledObjectObserver
{
    [Header("Required Components")]
    [SerializeField] private PropDestroyer mainPropDestroyer;
    [SerializeField] private RagDollController ragDollController;
    [SerializeField] private WendigoHeadTarget headTarget;
    [SerializeField] private WendigoPooledObject pool;

    [SerializeField] private FunctionTimer timer;
    public FunctionTimer Timer => timer;

    [SerializeField] private WendigoData data;
    public WendigoData Data => data;

    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public StateMachine stateMachine;

    [HideInInspector] public WendigoRotator rotator;
    [HideInInspector] public WendigoMover mover;
    
    [HideInInspector] public bool testDeath;

    private void Awake() => pool.Subscribe(this);

    public void OnSpawn() => Initialize();

    public void Initialize()
    {
        InitializeComponents();
        WendigoStateMachineInstaller.SetUp(this, stateMachine);

        healthSystem.SubscribeOnRagdoll(this);
    }

    private void Update()
    {
        if (stateMachine != null)
            stateMachine.Tick();
    }

    private void InitializeComponents()
    {
        rotator = GetComponent<WendigoRotator>();
        mover = GetComponent<WendigoMover>();
        healthSystem = GetComponent<HealthSystem>();

        stateMachine = new StateMachine();
    }

    public void SetTarget(Transform target)
    {
        Data.Target = target;

        //target.Set(rawTarget);
        //headTarget.SetTarget(rawTarget);
    }

    public void TriggerRagdoll(Vector3 impact, Vector3 hitPoint)
    {
        headTarget.SetTarget(null);
        ragDollController.TriggerRagdoll(impact, hitPoint);
        testDeath = true;
    }
}
