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

    public Transform Target => target;
    [SerializeField] private Transform target;
    
    private void Awake()
    {
        rotator = GetComponent<WendigoRotator>();
        mover = GetComponent<WendigoMover>();
        timer = GetComponent<FunctionTimer>();

        stateMachine = new StateMachine();

        var arrival = new Arrival(timer, this);
        var idle = new Idle();
        var chase = new Chase(rotator, mover, this);

        At(arrival, idle, HasNoTarget());
        At(arrival, chase, HasTarget());

        At(idle, chase, HasTarget());
        At(chase, idle, HasNoTarget());

        stateMachine.SetState(arrival);

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTarget() => () => target != null;
        Func<bool> HasNoTarget() => () => target == null;
    }

    private void Update() => stateMachine.Tick();

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
