using UnityEngine;
using System;

public static class WendigoStateMachineInstaller
{
    public static void SetUp(Wendigo wendigo, IStateMachine stateMachine)
    {
        IState disabled = new Disabled();
        IState arrival = new Arrival(wendigo);
        IState idle = new Idle();
        IState chase = new Chase(wendigo);
        IState dead = new Dead();

        Add(disabled, arrival, IsActive());
        Add(arrival, idle, IsArrived());

        Add(idle, chase, HasTarget());
        Add(chase, idle, HasNoTarget());

        Add(idle, dead, IsDead());
        Add(chase, dead, IsDead());

        stateMachine.SetState(arrival);

        void Add(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> IsActive() => () => wendigo.Data.IsActive;
        Func<bool> IsArrived() => () => wendigo.Data.IsArrived;
        Func<bool> HasTarget() => () => wendigo.Data.Target != null;
        Func<bool> HasNoTarget() => () => wendigo.Data.Target == null;
        Func<bool> IsDead() => () => !wendigo.HealthSystem.IsAlive() || wendigo.testDeath;
    }
}