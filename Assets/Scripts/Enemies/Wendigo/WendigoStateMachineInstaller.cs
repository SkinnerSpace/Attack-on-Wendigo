using UnityEngine;
using System;

public static class WendigoStateMachineInstaller
{
    public static void SetUp(Wendigo wendigo, StateMachine stateMachine)
    {
        var arrival = new Arrival(wendigo);
        var idle = new Idle();
        var chase = new Chase(wendigo.rotator, wendigo.mover, wendigo.target);
        var dead = new Dead();

        Add(arrival, idle, HasNoTarget());
        Add(arrival, chase, HasTarget());

        Add(idle, chase, HasTarget());
        Add(chase, idle, HasNoTarget());

        Add(idle, dead, IsDead());
        Add(chase, dead, IsDead());

        stateMachine.SetState(arrival);

        void Add(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTarget() => () => wendigo.target.Exist;
        Func<bool> HasNoTarget() => () => !wendigo.target.Exist;
        Func<bool> IsDead() => () => !wendigo.healthSystem.IsAlive() || wendigo.testDeath;
    }
}