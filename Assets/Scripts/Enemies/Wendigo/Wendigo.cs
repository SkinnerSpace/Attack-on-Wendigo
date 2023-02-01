using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo : MonoBehaviour, IDamageable, IHealthObserver
{
    private HealthSystem healthSystem;
    private StateMachine stateMachine;

    [SerializeField] private RagDollController ragDollController;

    private WendigoRotator rotator;
    private WendigoMover mover;
    private FunctionTimer timer;

    public WendigoTarget target { get; private set; }
    [SerializeField] private WendigoHeadTarget headTarget;

    private bool testDeath;

    private void Awake()
    {
        InitializeComponents();
        SetUpStateMachine();

        healthSystem.Subscribe(this);
    }

    private void FixedUpdate() => stateMachine.Tick();

    private void InitializeComponents()
    {
        rotator = GetComponent<WendigoRotator>();
        mover = GetComponent<WendigoMover>();
        timer = GetComponent<FunctionTimer>();
        healthSystem = GetComponent<HealthSystem>();

        target = new WendigoTarget();
    }

    private void SetUpStateMachine()
    {
        stateMachine = new StateMachine();

        var arrival = new Arrival(timer, this);
        var idle = new Idle();
        var chase = new Chase(rotator, mover, target);
        var dead = new Dead();

        Add(arrival, idle, HasNoTarget());
        Add(arrival, chase, HasTarget());

        Add(idle, chase, HasTarget());
        Add(chase, idle, HasNoTarget());

        Add(idle, dead, IsDead());
        Add(chase, dead, IsDead());

        stateMachine.SetState(arrival);

        void Add(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTarget() => () => target.Exist;
        Func<bool> HasNoTarget() => () => !target.Exist;
        Func<bool> IsDead() => () => !healthSystem.IsAlive() || testDeath;
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) HasDied();
    }

    public void SetTarget(Transform target)
    {
        this.target.Set(target);
        headTarget.SetTarget(target);
    }

    public void HasDied()
    {
        ragDollController.SwitchOn();
        testDeath = true;
    }
}
