using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo : MonoBehaviour, IDamageable
{
    private StateMachine stateMachine;

    private WendigoRotator rotator;
    private WendigoMover mover;
    private FunctionTimer timer;

    public WendigoTarget target { get; private set; }
    [SerializeField] private WendigoHeadTarget headTarget;
    
    private void Awake()
    {
        InitializeComponents();
        SetUpStateMachine();
    }

    private void FixedUpdate() => stateMachine.Tick();

    private void InitializeComponents()
    {
        rotator = GetComponent<WendigoRotator>();
        mover = GetComponent<WendigoMover>();
        timer = GetComponent<FunctionTimer>();

        target = new WendigoTarget();
    }

    private void SetUpStateMachine()
    {
        stateMachine = new StateMachine();

        var arrival = new Arrival(timer, this);
        var idle = new Idle();
        var chase = new Chase(rotator, mover, target);

        At(arrival, idle, HasNoTarget());
        At(arrival, chase, HasTarget());

        At(idle, chase, HasTarget());
        At(chase, idle, HasNoTarget());

        stateMachine.SetState(arrival);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTarget() => () => target.Exist;
        Func<bool> HasNoTarget() => () => !target.Exist;
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target.Set(target);
        headTarget.SetTarget(target);
    }
}

public class WendigoTarget
{
    public bool Exist => target != null;
    public Vector3 Position => target.position;
    private Transform target;

    public void Set(Transform target) => this.target = target;
}
