using UnityEngine;
using System;

public static class WendigoStateMachineInstaller
{
    public static void SetUp(Wendigo wendigo, StateMachine stateMachine)
    {
        var arrival = new Arrival(wendigo);
        var idle = new Idle();
        var chase = new Chase(wendigo);
        var dead = new Dead();

        Add(arrival, idle, IsArrived());

        Add(idle, chase, HasTarget());
        Add(chase, idle, HasNoTarget());

        Add(idle, dead, IsDead());
        Add(chase, dead, IsDead());

        stateMachine.SetState(arrival);

        void Add(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> IsArrived() => () => wendigo.Data.IsArrived;
        Func<bool> HasTarget() => () => wendigo.Data.Target != null;
        Func<bool> HasNoTarget() => () => wendigo.Data.Target == null;
        Func<bool> IsDead() => () => !wendigo.HealthSystem.IsAlive() || wendigo.testDeath;
    }
}