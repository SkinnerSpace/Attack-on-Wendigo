using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo : MonoBehaviour, IRagdoll
{
    [Header("Required Components")]
    [SerializeField] private PropDestroyer mainPropDestroyer;
    [SerializeField] private RagDollController ragDollController;
    [SerializeField] private WendigoHeadTarget headTarget;

    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public StateMachine stateMachine;

    [HideInInspector] public WendigoRotator rotator;
    [HideInInspector] public WendigoMover mover;
    [HideInInspector] public FunctionTimer timer;

    [HideInInspector] public WendigoTarget target { get; private set; }
    
    [HideInInspector] public bool testDeath;

    private void Awake()
    {
        InitializeComponents();
        WendigoStateMachineInstaller.SetUp(this, stateMachine);

        healthSystem.SubscribeOnRagdoll(this);
    }

    private void FixedUpdate() => stateMachine.Tick();

    private void InitializeComponents()
    {
        rotator = GetComponent<WendigoRotator>();
        mover = GetComponent<WendigoMover>();
        timer = GetComponent<FunctionTimer>();
        healthSystem = GetComponent<HealthSystem>();

        stateMachine = new StateMachine();
        target = new WendigoTarget();
    }

    public void SetTarget(Transform rawTarget)
    {
        target.Set(rawTarget);
        headTarget.SetTarget(rawTarget);
    }

    public void TriggerRagdoll(Vector3 impact, Vector3 hitPoint)
    {
        headTarget.SetTarget(null);
        ragDollController.TriggerRagdoll(impact, hitPoint);
        testDeath = true;
    }
}
